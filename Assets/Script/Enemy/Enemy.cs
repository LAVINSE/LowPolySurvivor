using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    #region ����
    [Header("=====> Enemy ���� <=====")]
    [SerializeField] protected float moveSpeed = 0;
    [SerializeField] protected float maxHp = 0;
    [SerializeField] protected float attackRange = 0;
    [SerializeField] protected float attackDelay = 0;

    protected float Delay = 0;
    #endregion // ����

    #region ������Ƽ
    public bool IsTracking { get; set; } = false;
    public bool IsAttack { get; set; } = false;
    public bool IsDie { get; set; } = false;

    public PlayerMain Player;
    #endregion // ������Ƽ

    #region �Լ�
    /** �ʱ�ȭ */
    private void Start()
    {
        Delay = attackDelay;
    }

    /** �������� �ش� */
    protected void TakeDamage()
    {
        // Do something
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
