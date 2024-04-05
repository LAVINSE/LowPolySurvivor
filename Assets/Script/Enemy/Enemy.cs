using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

// ����ؼ� ����ϱ�
public class Enemy : MonoBehaviour
{
    #region ����
    [Header("=====> ������ <=====")]
    [SerializeField] protected EnemyDataSO enemyDataSO;
    [SerializeField] protected List<ItemDataSO> dropItemList = new List<ItemDataSO>();

    [Header("=====> Enemy ���� <=====")]
    [SerializeField] protected float attackRange = 0f;
    [SerializeField] private Transform rootTransform;
    [SerializeField] private float correctStoppingDistance = 0.26f; // ������ 0.26f ���Ϸ�

    [Header("=====> �ν����� Ȯ�� <=====")]
    [Space]
    [SerializeField] protected float moveSpeed = 0;
    [SerializeField] protected float maxHp = 0;
    [SerializeField] protected float attackDelay = 0;
    [SerializeField] protected float attackdamage = 0;

    protected Animator animator;
    protected Rigidbody rigid;
    protected NavMeshAgent navMeshAgent;

    protected bool isDie = false;
    #endregion // ����

    #region ������Ƽ
    public Transform MeshTransform => rootTransform;

    public bool IsTracking { get; set; } = false;
    public bool IsAttack { get; set; } = false;

    public float CurrentHp { get; set; } = 0;

    public PlayerMain Player;
    #endregion // ������Ƽ

    #region �Լ�
    private void OnDrawGizmos()
    {
        Debug.DrawRay(new Vector3(rootTransform.transform.position.x, rootTransform.transform.position.y,
            rootTransform.transform.position.z),
            this.transform.forward * attackRange, Color.green);
    }

    /** �ʱ�ȭ */
    public virtual void Awake()
    {
        rigid = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();
        navMeshAgent = GetComponent<NavMeshAgent>();

        // TODO : �׽�Ʈ
        Init(0);

        navMeshAgent.speed = moveSpeed;
        navMeshAgent.stoppingDistance = attackRange + correctStoppingDistance;
    }

    /** �� ������ ���� */
    public void Init(int stageLevel)
    {
        moveSpeed = enemyDataSO.enemyDataStruct[stageLevel].moveSpeed;
        maxHp = enemyDataSO.enemyDataStruct[stageLevel].maxHp;
        attackDelay = enemyDataSO.enemyDataStruct[stageLevel].attackDelay;
        attackdamage = enemyDataSO.enemyDataStruct[stageLevel].attackDamage;

        CurrentHp = maxHp;
    }

    /** NavMesh �÷��̾ �����Ѵ� */
    public void NavMeshSetDestination()
    {
        navMeshAgent.SetDestination(Player.transform.position);
    }

    /** ���� ��Ÿ��� ���Դ��� Ȯ���Ѵ� */
    public bool CheckattackChangeRange()
    {
        RaycastHit hit;
        Vector3 pos = new Vector3(this.transform.position.x, rootTransform.position.y, this.transform.position.z);

        // �� �������� ����ĳ��Ʈ �߻�
        if (Physics.Raycast(rootTransform.position, Player.transform.position - pos, out hit, attackRange))
        {
            // ����ĳ��Ʈ�� ������ �浹�� ���
            if (hit.collider.CompareTag("Player"))
            {
                return true;
            }
        }

        return false;
    }

    /** �÷��̾�� �Ÿ��� üũ�ϰ� �����Ѵ� */
    public void TargetSetting()
    {
        RaycastHit hit;

        Vector3 pos = new Vector3(this.transform.position.x, rootTransform.position.y, this.transform.position.z);

        // �� �������� ����ĳ��Ʈ �߻�
        if (Physics.Raycast(rootTransform.position, Player.transform.position - pos, out hit, attackRange))
        {
            // ����ĳ��Ʈ�� ������ �浹�� ���
            if (hit.collider.CompareTag("Player"))
            {
                if (!IsAttack)
                {
                    Debug.Log(" ���� ���� ");
                    Attack();
                }
            }
        }
    }

    /** �����Ѵ� */
    public virtual void Attack()
    {

    }

    /** �������� �޴´� */
    public virtual void TakeDamage(float damage)
    {
        animator.SetTrigger("hitTrigger");

        CurrentHp -= damage;

        if(CurrentHp <= 0)
        { 
            // ����
            Die();
        }
    }

    /** ���� ���� */
    public virtual void Die()
    {
        // TODO : �׽�Ʈ�� �ڵ�
        if(isDie == true) { return; }

        isDie = true;
        animator.SetBool("isDie", true);
    }

    /** ��� ������ ����Ʈ���� �������� �����Ѵ� */
    protected void InstantiateDropItem(Vector3 spawnPosition)
    {
        // ��� ������ ����Ʈ�� ��ȯ�Ѵ�
        List<ItemDataSO> dropItemList = GetDropItem();

        if (dropItemList.Count == 0) { return; }

        foreach (ItemDataSO dropItemData in dropItemList)
        {
            // ������ �������� �����Ѵ�
            GameObject dropItem = Instantiate(dropItemData.itemPrefab, spawnPosition, Quaternion.identity);

            // ����Ҷ� ���� ���� ����� ���� ����
            float dropForce = 5f;
            Vector3 dropDirection = new Vector3(UnityEngine.Random.Range(-1f, 1f), 3f, UnityEngine.Random.Range(-1f, 1f));
            dropItem.GetComponent<Rigidbody>().AddForce(dropDirection * dropForce, ForceMode.Impulse);
        }
    }

    /** ��� ������ ����Ʈ�� ��ȯ�Ѵ� */
    private List<ItemDataSO> GetDropItem()
    {
        // �������� ���� ����Ʈ ����
        List<ItemDataSO> pickitems = new List<ItemDataSO>();

        foreach(ItemDataSO item in dropItemList)
        {
            // ���� ���ڰ� �������� ��� Ȯ������ �۰ų� �������
            if(UnityEngine.Random.Range(1, 101) <= item.dropChance)
            {
                // �ش� �������� �߰��Ѵ�
                pickitems.Add(item);
            }
        }

        // ���� ������ ����Ʈ�� �������� �����Ѵٸ�
        if(pickitems.Count > 0)
        {
            // ������ ����Ʈ ��ȯ
            return pickitems;
        }

        // �������� ������� null ��ȯ
        return null;
    }
    #endregion // �Լ�

    #region �ڷ�ƾ
    /** ��Ÿ�� ���� */
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
    #endregion // �ڷ�ƾ
}
