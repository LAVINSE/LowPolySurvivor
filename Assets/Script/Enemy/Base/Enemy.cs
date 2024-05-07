using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


// 상속해서 사용하기
public class Enemy : MonoBehaviour
{
    #region 변수
    [Header("=====> 데이터 <=====")]
    [SerializeField] protected EnemyDataSO enemyDataSO; // 데이터
    [SerializeField] protected List<ItemDropSO> dropItemList = new List<ItemDropSO>(); // 드랍 아이템

    [Header("=====> Enemy 변수 <=====")]
    [SerializeField] private Transform rootTransform;

    [Header("=====> 인스펙터 확인 <=====")]
    [Space]
    [SerializeField] protected float moveSpeed = 0;
    [SerializeField] protected float maxHp = 0;
    [SerializeField] protected float attackDelay = 0;
    [SerializeField] protected float attackDamage = 0;
    [SerializeField] protected float attackRange = 0f;

    protected Rigidbody rigid;
    
    protected bool isDie = false; // 죽음 확인
    private bool isKnockBack = false;
    private float knockbackTimer = 0f; // 넉백 지속 시간을 계산하는 타이머
    public float knockbackDuration = 0.5f; // 넉백 지속 시간
    #endregion // 변수

    #region 프로퍼티
    public Animator Animator { get; set; }
    public NavMeshAgent navMeshAgent { get; set; }
    public Transform MeshTransform => rootTransform; // 위치 정보
    public Rigidbody Rigid => rigid;

    public bool IsTracking { get; set; } = false; // 추적 확인
    public bool IsAttack { get; set; } = false; // 공격 확인
    public bool isDamage { get; set; } = false;

    public float MaxHp => maxHp;
    public float CurrentHp { get; set; } = 0; // 현재 체력

    public PlayerMain Player; // 플레이어
    #endregion // 프로퍼티

    #region 함수
    public virtual void OnDrawGizmos()
    {
        Debug.DrawRay(new Vector3(this.transform.position.x, rootTransform.transform.position.y,
            rootTransform.transform.position.z),
            this.transform.forward * attackRange, Color.green);
    }

    /** 초기화 */
    public virtual void Awake()
    {
        rigid = GetComponent<Rigidbody>();
        Animator = GetComponent<Animator>();
        navMeshAgent = GetComponent<NavMeshAgent>();
    }

    /** 초기화 */
    protected virtual void OnEnable()
    {
        
    }
     
    /** 초기화 */
    protected virtual void OnDisable()
    {
        StopAllCoroutines();
    }

    /** 초기화 상태를 갱신한다 */
    private void Update()
    {
        // 넉백 상태인 경우 타이머를 업데이트하고 지속 시간이 끝나면 넉백 상태 해제
        if (isKnockBack)
        {
            knockbackTimer -= Time.deltaTime;

            if (knockbackTimer <= 0)
            {
                rigid.velocity = Vector3.zero;
                navMeshAgent.isStopped = false;
                isKnockBack = false;
            }
        }
    } 

    /** 적 데이터 세팅 */
    public void Init(PlayerMain playerMain, int stageLevel = 0)
    {
        this.Player= playerMain;

        moveSpeed = enemyDataSO.enemyDataStruct[stageLevel].moveSpeed;
        maxHp = enemyDataSO.enemyDataStruct[stageLevel].maxHp;
        attackDelay = enemyDataSO.enemyDataStruct[stageLevel].attackDelay;
        attackDamage = enemyDataSO.enemyDataStruct[stageLevel].attackDamage;

        attackRange = enemyDataSO.attackRange;
        CurrentHp = maxHp;

        navMeshAgent.speed = moveSpeed;
    }

    /** NavMesh 플레이어를 추적한다 */
    public void NavMeshSetDestination()
    {
        if(this.transform.position.y < -10f)
        {
            this.gameObject.SetActive(false);
        }

        navMeshAgent.SetDestination(Player.transform.position);
    }

    /** 공격 사거리에 들어왔는지 확인한다 */
    public bool CheckAttackRange()
    {
        RaycastHit hit;

        // 적 방향으로 레이캐스트 발사
        if (Physics.Raycast(rootTransform.position, this.transform.forward, out hit, attackRange))
        {
            // 레이캐스트가 적에게 충돌한 경우
            if (hit.collider.CompareTag("Player"))
            {
                return true;
            }
        }

        return false;
    }

    /** 플레이어와 거리를 체크하고 공격한다 */
    public void TargetSetting()
    {
        RaycastHit hit;

        // 적 방향으로 레이캐스트 발사
        if (Physics.Raycast(rootTransform.position, this.transform.forward, out hit, attackRange))
        {
            // 레이캐스트가 적에게 충돌한 경우
            if (hit.collider.CompareTag("Player"))
            {
                if (!IsAttack)
                {
                    Attack(hit);
                }
            }
        }
    }

    /** 공격한다 */
    public virtual void Attack(RaycastHit hit)
    {
        
    }

    /** 데미지를 받는다 */
    public virtual void TakeDamage(float damage, float knockBackPower = 0f, bool isKnockBack = false)
    {
        if(isDamage == true) { return; }

        isDamage = true;

        Animator.SetTrigger("hitTrigger");

        if(isKnockBack == true && this.isKnockBack == false)
        {
            KnockBack();
        }

        CurrentHp -= Mathf.RoundToInt(damage);

        if (CurrentHp <= 0)
        { 
            // 죽음
            Die();
        }

        isDamage = false;
    }

    public void KnockBack()
    {
        rigid.velocity = Vector3.zero;
        Vector3 direction = this.transform.position - Player.transform.position;
        navMeshAgent.isStopped = true;
        rigid.AddForce(direction.normalized * 3f, ForceMode.Impulse);

        isKnockBack = true;
        knockbackTimer = knockbackDuration;
    }

    /** 몬스터 죽음 */
    public virtual void Die()
    {
        // TODO : 테스트용 코드
        if(isDie == true) { return; }

        isDie = true;
        Animator.SetBool("isDie", true);
        //SpawnManager.SpawnCount--;
    }

    /** 드랍 아이템 리스트에서 아이템을 생성한다 */
    protected void InstantiateDropItem(Vector3 spawnPosition)
    {
        // 드랍 아이템 리스트를 반환한다
        List<ItemDropSO> dropItemList = GetDropItem();

        // 플레이어 위치 + 0.2 보정값
        spawnPosition.y = Player.transform.position.y + 0.2f;

        if (dropItemList.Count == 0) { return; }

        foreach (ItemDropSO dropItemData in dropItemList)
        {
            // 아이템 프리팹을 생성한다
            GameObject dropItem = GameManager.Instance.PoolManager.GetItem((int)dropItemData.itemType, spawnPosition);
        }
    }

    /** 드랍 아이템 리스트를 반환한다 */
    private List<ItemDropSO> GetDropItem()
    {
        // 아이템을 담을 리스트 생성
        List<ItemDropSO> pickitems = new List<ItemDropSO>();

        foreach(ItemDropSO item in dropItemList)
        {
            // 랜덤 숫자가 아이템의 드랍 확률보다 작거나 같을경우
            if(UnityEngine.Random.Range(1, 101) <= item.dropChance)

            {
                // 해당 아이템을 추가한다
                pickitems.Add(item);
            }
        }

        // 만약 아이템 리스트에 아이템이 존재한다면
        if(pickitems.Count > 0)
        {
            // 아이템 리스트 반환
            return pickitems;
        }

        // 아이템이 없을경우 null 반환
        return null;
    }
    #endregion // 함수

    #region 코루틴
    /** 쿨타임 적용 */
    protected IEnumerator CoolDownCO(float CoolTime, Action callback)
    {
        float CurrentTime = 0.0f;
        CurrentTime = CoolTime;

        while (CurrentTime > 0.0f)
        {
            CurrentTime -= Time.deltaTime;

            yield return null;
        }

        callback?.Invoke();
    }
    #endregion // 코루틴
}
