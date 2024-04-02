using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;

// ����ؼ� ����ϱ�
public class Enemy : MonoBehaviour
{
    #region ����
    [Header("=====> ������ <=====")]
    [SerializeField] protected EnemyDataSO enemyDataSO;
    [SerializeField] protected List<ItemDataSO> dropItemList = new List<ItemDataSO>();

    [Header("=====> Enemy ���� <=====")]
    [SerializeField] protected int moveSpeed = 0;
    [SerializeField] protected float maxHp = 0;
    [SerializeField] protected float attackDelay = 0;
    [SerializeField] private GameObject meshBody;

    [Header("=====> �ν����� Ȯ�� <=====")]
    [Space]
    [SerializeField] protected float attackRange = 0;

    protected float Delay = 0;
    protected Animator animator;
    protected bool isDie = false;
    #endregion // ����

    #region ������Ƽ
    public Transform MeshTransform { get; set; }

    public bool IsTracking { get; set; } = false;
    public bool IsAttack { get; set; } = false;
    public bool IsDie { get; set; } = false;

    public float CurrentHp { get; set; } = 0;

    public PlayerMain Player;
    #endregion // ������Ƽ

    #region �Լ�
    private void OnDrawGizmos()
    {
        Debug.DrawRay(this.transform.position, this.transform.forward * attackRange, Color.green);
    }

    /** �ʱ�ȭ */
    public virtual void Awake()
    {
        animator = GetComponent<Animator>();
        MeshTransform = meshBody.transform;
        // TODO : �׽�Ʈ
        Init(0);
    }

    /** �� ������ ���� */
    public void Init(int stageLevel)
    {
        moveSpeed = enemyDataSO.enemyDataStruct[stageLevel].moveSpeed;
        maxHp = enemyDataSO.enemyDataStruct[stageLevel].maxHp;
        attackDelay = enemyDataSO.enemyDataStruct[stageLevel].attackDelay;

        CurrentHp = maxHp;
        Delay = attackDelay;
    }

    /** �÷��̾�� �Ÿ��� üũ�ϰ� �����Ѵ� */
    public void TargetSetting()
    {
        float distance = Vector3.Distance(Player.transform.position, this.transform.position);

        // �����̰� 0���� Ŭ ���
        if (attackDelay >= 0)
        {
            // ������ ����
            attackDelay -= Time.deltaTime;
            Debug.Log(" �ð� ���� ");
        }

        // ���� �÷��̾� �Ÿ��� �� ���ݻ�Ÿ� ���� �۰ų� �������, �������� �ƴҰ��
        if (attackDelay <= 0 && distance <= attackRange && !IsAttack)
        {
            Debug.Log(" ���� ���� ");
            // �����Ѵ� 
            Attack();
        }
    }

    /** �����Ѵ� */
    public virtual void Attack()
    {

    }

    /** �������� �޴´� */
    public void TakeDamage(float damage)
    {
        animator.SetTrigger("hitTrigger");

        CurrentHp -= damage;

        if(CurrentHp <= 0)
        {
            // ����
            Die();
        }

        Debug.Log(" ���� ");
    }

    /** ���� ���� */
    private void Die()
    {
        // TODO : �׽�Ʈ�� �ڵ�
        if(IsDie == true) { return; }

        isDie = true;
        animator.SetBool("isDie", true);

        // TODO : �߷� X, �ݶ��̴� isTrigger ���� �����ؾߵ� 

        // ������ ���
        InstantiateDropItem(this.transform.position);

        Debug.Log(" ���� ");

        // TODO : ��Ȱ��ȭ ó��, �׽�Ʈ������ ���� ó����
        Destroy(this.gameObject);
    }

    /** ��� ������ ����Ʈ�� ��ȯ�Ѵ� */
    private List<ItemDataSO> GetDropItem()
    {
        // �������� ���� ����Ʈ ����
        List<ItemDataSO> pickitems = new List<ItemDataSO>();

        foreach(ItemDataSO item in dropItemList)
        {
            // ���� ���ڰ� �������� ��� Ȯ������ �۰ų� �������
            if(Random.Range(1, 101) <= item.dropChance)
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

    /** ��� ������ ����Ʈ���� �������� �����Ѵ� */
    private void InstantiateDropItem(Vector3 spawnPosition)
    {
        // ��� ������ ����Ʈ�� ��ȯ�Ѵ�
        List<ItemDataSO> dropItemList = GetDropItem();

        if(dropItemList.Count == 0) { return; }


        foreach(ItemDataSO dropItemData in dropItemList)
        {
            // ������ �������� �����Ѵ�
            GameObject dropItem = Instantiate(dropItemData.itemPrefab, spawnPosition, Quaternion.identity);

            // ����Ҷ� ���� ���� ����� ���� ����
            float dropForce = 5f;
            Vector3 dropDirection = new Vector3(Random.Range(-1f, 1f), 3f, Random.Range(-1f, 1f));
            dropItem.GetComponent<Rigidbody>().AddForce(dropDirection * dropForce, ForceMode.Impulse);
        }
    }
    #endregion // �Լ�
}
