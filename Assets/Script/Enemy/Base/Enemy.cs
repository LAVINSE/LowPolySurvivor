using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


// ����ؼ� ����ϱ�
public class Enemy : MonoBehaviour
{
    #region ����
    [Header("=====> ������ <=====")]
    [SerializeField] protected EnemyDataSO enemyDataSO; // ������
    [SerializeField] protected List<ItemDropSO> dropItemList = new List<ItemDropSO>(); // ��� ������

    [Header("=====> Enemy ���� <=====")]
    [SerializeField] private Transform rootTransform;

    [Header("=====> �ν����� Ȯ�� <=====")]
    [Space]
    [SerializeField] protected float moveSpeed = 0;
    [SerializeField] protected float maxHp = 0;
    [SerializeField] protected float attackDelay = 0;
    [SerializeField] protected float attackDamage = 0;
    [SerializeField] protected float attackRange = 0f;

    protected Rigidbody rigid;
    
    protected bool isDie = false; // ���� Ȯ��
    private bool isKnockBack = false;
    private float knockbackTimer = 0f; // �˹� ���� �ð��� ����ϴ� Ÿ�̸�
    public float knockbackDuration = 0.5f; // �˹� ���� �ð�
    #endregion // ����

    #region ������Ƽ
    public Animator Animator { get; set; }
    public NavMeshAgent navMeshAgent { get; set; }
    public Transform MeshTransform => rootTransform; // ��ġ ����
    public Rigidbody Rigid => rigid;

    public bool IsTracking { get; set; } = false; // ���� Ȯ��
    public bool IsAttack { get; set; } = false; // ���� Ȯ��
    public bool isDamage { get; set; } = false;

    public float MaxHp => maxHp;
    public float CurrentHp { get; set; } = 0; // ���� ü��

    public PlayerMain Player; // �÷��̾�
    #endregion // ������Ƽ

    #region �Լ�
    public virtual void OnDrawGizmos()
    {
        Debug.DrawRay(new Vector3(this.transform.position.x, rootTransform.transform.position.y,
            rootTransform.transform.position.z),
            this.transform.forward * attackRange, Color.green);
    }

    /** �ʱ�ȭ */
    public virtual void Awake()
    {
        rigid = GetComponent<Rigidbody>();
        Animator = GetComponent<Animator>();
        navMeshAgent = GetComponent<NavMeshAgent>();
    }

    /** �ʱ�ȭ */
    protected virtual void OnEnable()
    {
        
    }
     
    /** �ʱ�ȭ */
    protected virtual void OnDisable()
    {
        StopAllCoroutines();
    }

    /** �ʱ�ȭ ���¸� �����Ѵ� */
    private void Update()
    {
        // �˹� ������ ��� Ÿ�̸Ӹ� ������Ʈ�ϰ� ���� �ð��� ������ �˹� ���� ����
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

    /** �� ������ ���� */
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

    /** NavMesh �÷��̾ �����Ѵ� */
    public void NavMeshSetDestination()
    {
        if(this.transform.position.y < -10f)
        {
            this.gameObject.SetActive(false);
        }

        navMeshAgent.SetDestination(Player.transform.position);
    }

    /** ���� ��Ÿ��� ���Դ��� Ȯ���Ѵ� */
    public bool CheckAttackRange()
    {
        RaycastHit hit;

        // �� �������� ����ĳ��Ʈ �߻�
        if (Physics.Raycast(rootTransform.position, this.transform.forward, out hit, attackRange))
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

        // �� �������� ����ĳ��Ʈ �߻�
        if (Physics.Raycast(rootTransform.position, this.transform.forward, out hit, attackRange))
        {
            // ����ĳ��Ʈ�� ������ �浹�� ���
            if (hit.collider.CompareTag("Player"))
            {
                if (!IsAttack)
                {
                    Attack(hit);
                }
            }
        }
    }

    /** �����Ѵ� */
    public virtual void Attack(RaycastHit hit)
    {
        
    }

    /** �������� �޴´� */
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
            // ����
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

    /** ���� ���� */
    public virtual void Die()
    {
        // TODO : �׽�Ʈ�� �ڵ�
        if(isDie == true) { return; }

        isDie = true;
        Animator.SetBool("isDie", true);
        //SpawnManager.SpawnCount--;
    }

    /** ��� ������ ����Ʈ���� �������� �����Ѵ� */
    protected void InstantiateDropItem(Vector3 spawnPosition)
    {
        // ��� ������ ����Ʈ�� ��ȯ�Ѵ�
        List<ItemDropSO> dropItemList = GetDropItem();

        // �÷��̾� ��ġ + 0.2 ������
        spawnPosition.y = Player.transform.position.y + 0.2f;

        if (dropItemList.Count == 0) { return; }

        foreach (ItemDropSO dropItemData in dropItemList)
        {
            // ������ �������� �����Ѵ�
            GameObject dropItem = GameManager.Instance.PoolManager.GetItem((int)dropItemData.itemType, spawnPosition);
        }
    }

    /** ��� ������ ����Ʈ�� ��ȯ�Ѵ� */
    private List<ItemDropSO> GetDropItem()
    {
        // �������� ���� ����Ʈ ����
        List<ItemDropSO> pickitems = new List<ItemDropSO>();

        foreach(ItemDropSO item in dropItemList)
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
