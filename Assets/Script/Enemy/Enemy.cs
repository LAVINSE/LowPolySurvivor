using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

// 상속해서 사용하기
public class Enemy : MonoBehaviour
{
    #region 변수
    [Header("=====> 데이터 <=====")]
    [SerializeField] protected EnemyDataSO enemyDataSO;
    [SerializeField] protected List<ItemDataSO> dropItemList = new List<ItemDataSO>();

    [Header("=====> Enemy 변수 <=====")]
    [SerializeField] protected float attackRange = 0f;
    [SerializeField] private Transform rootTransform;
    [SerializeField] private float correctStoppingDistance = 0.26f; // 보정값 0.26f 이하로

    [Header("=====> 인스펙터 확인 <=====")]
    [Space]
    [SerializeField] protected float moveSpeed = 0;
    [SerializeField] protected float maxHp = 0;
    [SerializeField] protected float attackDelay = 0;
    [SerializeField] protected float attackdamage = 0;

    protected Animator animator;
    protected Rigidbody rigid;
    protected NavMeshAgent navMeshAgent;

    protected bool isDie = false;
    #endregion // 변수

    #region 프로퍼티
    public Transform MeshTransform => rootTransform;

    public bool IsTracking { get; set; } = false;
    public bool IsAttack { get; set; } = false;

    public float CurrentHp { get; set; } = 0;

    public PlayerMain Player;
    #endregion // 프로퍼티

    #region 함수
    private void OnDrawGizmos()
    {
        Debug.DrawRay(new Vector3(rootTransform.transform.position.x, rootTransform.transform.position.y,
            rootTransform.transform.position.z),
            this.transform.forward * attackRange, Color.green);
    }

    /** 초기화 */
    public virtual void Awake()
    {
        rigid = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();
        navMeshAgent = GetComponent<NavMeshAgent>();

        // TODO : 테스트
        Init(0);

        navMeshAgent.speed = moveSpeed;
        navMeshAgent.stoppingDistance = attackRange + correctStoppingDistance;
    }

    /** 적 데이터 세팅 */
    public void Init(int stageLevel)
    {
        moveSpeed = enemyDataSO.enemyDataStruct[stageLevel].moveSpeed;
        maxHp = enemyDataSO.enemyDataStruct[stageLevel].maxHp;
        attackDelay = enemyDataSO.enemyDataStruct[stageLevel].attackDelay;
        attackdamage = enemyDataSO.enemyDataStruct[stageLevel].attackDamage;

        CurrentHp = maxHp;
    }

    /** NavMesh 플레이어를 추적한다 */
    public void NavMeshSetDestination()
    {
        navMeshAgent.SetDestination(Player.transform.position);
    }

    /** 공격 사거리에 들어왔는지 확인한다 */
    public bool CheckattackChangeRange()
    {
        RaycastHit hit;
        Vector3 pos = new Vector3(this.transform.position.x, rootTransform.position.y, this.transform.position.z);

        // 적 방향으로 레이캐스트 발사
        if (Physics.Raycast(rootTransform.position, Player.transform.position - pos, out hit, attackRange))
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

        Vector3 pos = new Vector3(this.transform.position.x, rootTransform.position.y, this.transform.position.z);

        // 적 방향으로 레이캐스트 발사
        if (Physics.Raycast(rootTransform.position, Player.transform.position - pos, out hit, attackRange))
        {
            // 레이캐스트가 적에게 충돌한 경우
            if (hit.collider.CompareTag("Player"))
            {
                if (!IsAttack)
                {
                    Debug.Log(" 공격 시작 ");
                    Attack();
                }
            }
        }
    }

    /** 공격한다 */
    public virtual void Attack()
    {

    }

    /** 데미지를 받는다 */
    public virtual void TakeDamage(float damage)
    {
        animator.SetTrigger("hitTrigger");

        CurrentHp -= damage;

        if(CurrentHp <= 0)
        { 
            // 죽음
            Die();
        }
    }

    /** 몬스터 죽음 */
    public virtual void Die()
    {
        // TODO : 테스트용 코드
        if(isDie == true) { return; }

        isDie = true;
        animator.SetBool("isDie", true);
    }

    /** 드랍 아이템 리스트에서 아이템을 생성한다 */
    protected void InstantiateDropItem(Vector3 spawnPosition)
    {
        // 드랍 아이템 리스트를 반환한다
        List<ItemDataSO> dropItemList = GetDropItem();

        if (dropItemList.Count == 0) { return; }

        foreach (ItemDataSO dropItemData in dropItemList)
        {
            // 아이템 프리팹을 생성한다
            GameObject dropItem = Instantiate(dropItemData.itemPrefab, spawnPosition, Quaternion.identity);

            // 드랍할때 가할 힘의 세기와 방향 설정
            float dropForce = 5f;
            Vector3 dropDirection = new Vector3(UnityEngine.Random.Range(-1f, 1f), 3f, UnityEngine.Random.Range(-1f, 1f));
            dropItem.GetComponent<Rigidbody>().AddForce(dropDirection * dropForce, ForceMode.Impulse);
        }
    }

    /** 드랍 아이템 리스트를 반환한다 */
    private List<ItemDataSO> GetDropItem()
    {
        // 아이템을 담을 리스트 생성
        List<ItemDataSO> pickitems = new List<ItemDataSO>();

        foreach(ItemDataSO item in dropItemList)
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
