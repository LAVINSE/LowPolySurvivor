using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    #region ����
    [Header("=====> ������ <=====")]
    [SerializeField] protected EnemyDataSO enemyDataSO;

    [Header("=====> Enemy ���� <=====")]
    [SerializeField] protected int moveSpeed = 0;
    [SerializeField] protected int maxHp = 0;
    [SerializeField] protected float attackDelay = 0;

    [Space]
    [SerializeField] protected int currentHp = 0;
    [SerializeField] protected float attackRange = 0;

    protected float Delay = 0;
    protected Animator animator;
    #endregion // ����

    #region ������Ƽ
    public bool IsTracking { get; set; } = false;
    public bool IsAttack { get; set; } = false;
    public bool IsDie { get; set; } = false;

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

        // TODO : �׽�Ʈ
        Init(0);
    }

    /** �� ������ ���� */
    public void Init(int stageLevel)
    {
        moveSpeed = enemyDataSO.enemyDataStruct[stageLevel].moveSpeed;
        maxHp = enemyDataSO.enemyDataStruct[stageLevel].maxHp;
        attackDelay = enemyDataSO.enemyDataStruct[stageLevel].attackDelay;

        currentHp = maxHp;
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
    #endregion // �Լ�
}
