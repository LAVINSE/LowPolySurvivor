using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;

// 상속해서 사용하기
public class Enemy : MonoBehaviour
{
    #region 변수
    [Header("=====> 데이터 <=====")]
    [SerializeField] protected EnemyDataSO enemyDataSO;
    [SerializeField] protected List<ItemDataSO> dropItemList = new List<ItemDataSO>();

    [Header("=====> Enemy 변수 <=====")]
    [SerializeField] protected int moveSpeed = 0;
    [SerializeField] protected float maxHp = 0;
    [SerializeField] protected float attackDelay = 0;
    [SerializeField] private GameObject meshBody;

    [Header("=====> 인스펙터 확인 <=====")]
    [Space]
    [SerializeField] protected float attackRange = 0;

    protected float Delay = 0;
    protected Animator animator;
    protected bool isDie = false;
    #endregion // 변수

    #region 프로퍼티
    public Transform MeshTransform { get; set; }

    public bool IsTracking { get; set; } = false;
    public bool IsAttack { get; set; } = false;
    public bool IsDie { get; set; } = false;

    public float CurrentHp { get; set; } = 0;

    public PlayerMain Player;
    #endregion // 프로퍼티

    #region 함수
    private void OnDrawGizmos()
    {
        Debug.DrawRay(this.transform.position, this.transform.forward * attackRange, Color.green);
    }

    /** 초기화 */
    public virtual void Awake()
    {
        animator = GetComponent<Animator>();
        MeshTransform = meshBody.transform;
        // TODO : 테스트
        Init(0);
    }

    /** 적 데이터 세팅 */
    public void Init(int stageLevel)
    {
        moveSpeed = enemyDataSO.enemyDataStruct[stageLevel].moveSpeed;
        maxHp = enemyDataSO.enemyDataStruct[stageLevel].maxHp;
        attackDelay = enemyDataSO.enemyDataStruct[stageLevel].attackDelay;

        CurrentHp = maxHp;
        Delay = attackDelay;
    }

    /** 플레이어와 거리를 체크하고 공격한다 */
    public void TargetSetting()
    {
        float distance = Vector3.Distance(Player.transform.position, this.transform.position);

        // 딜레이가 0보다 클 경우
        if (attackDelay >= 0)
        {
            // 딜레이 감소
            attackDelay -= Time.deltaTime;
            Debug.Log(" 시간 감소 ");
        }

        // 적과 플레이어 거리가 적 공격사거리 보다 작거나 같을경우, 공격중이 아닐경우
        if (attackDelay <= 0 && distance <= attackRange && !IsAttack)
        {
            Debug.Log(" 공격 시작 ");
            // 공격한다 
            Attack();
        }
    }

    /** 공격한다 */
    public virtual void Attack()
    {

    }

    /** 데미지를 받는다 */
    public void TakeDamage(float damage)
    {
        animator.SetTrigger("hitTrigger");

        CurrentHp -= damage;

        if(CurrentHp <= 0)
        {
            // 죽음
            Die();
        }

        Debug.Log(" 맞음 ");
    }

    /** 몬스터 죽음 */
    private void Die()
    {
        // TODO : 테스트용 코드
        if(IsDie == true) { return; }

        isDie = true;
        animator.SetBool("isDie", true);

        // TODO : 중력 X, 콜라이더 isTrigger 해제 설정해야됨 

        // 아이템 드랍
        InstantiateDropItem(this.transform.position);

        Debug.Log(" 죽음 ");

        // TODO : 비활성화 처리, 테스트용으로 삭제 처리함
        Destroy(this.gameObject);
    }

    /** 드랍 아이템 리스트를 반환한다 */
    private List<ItemDataSO> GetDropItem()
    {
        // 아이템을 담을 리스트 생성
        List<ItemDataSO> pickitems = new List<ItemDataSO>();

        foreach(ItemDataSO item in dropItemList)
        {
            // 랜덤 숫자가 아이템의 드랍 확률보다 작거나 같을경우
            if(Random.Range(1, 101) <= item.dropChance)
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

    /** 드랍 아이템 리스트에서 아이템을 생성한다 */
    private void InstantiateDropItem(Vector3 spawnPosition)
    {
        // 드랍 아이템 리스트를 반환한다
        List<ItemDataSO> dropItemList = GetDropItem();

        if(dropItemList.Count == 0) { return; }


        foreach(ItemDataSO dropItemData in dropItemList)
        {
            // 아이템 프리팹을 생성한다
            GameObject dropItem = Instantiate(dropItemData.itemPrefab, spawnPosition, Quaternion.identity);

            // 드랍할때 가할 힘의 세기와 방향 설정
            float dropForce = 5f;
            Vector3 dropDirection = new Vector3(Random.Range(-1f, 1f), 3f, Random.Range(-1f, 1f));
            dropItem.GetComponent<Rigidbody>().AddForce(dropDirection * dropForce, ForceMode.Impulse);
        }
    }
    #endregion // 함수
}
