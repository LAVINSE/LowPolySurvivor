using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeholderEnemy : Enemy
{
    #region ����
    [Header("=====> Beholder ���� <=====")]
    [SerializeField] private float attackDamage = 0;
    [SerializeField] private float attackRange = 0;
    [SerializeField] private float attackDelay = 0;

    private float Delay = 0;
    #endregion // ����

    #region �Լ�
    private void Awake()
    {
        Delay = attackDelay;
    }

    /** �÷��̾�� �Ÿ��� üũ�ϰ� �����Ѵ� */
    public override void TargetSetting()
    {
        base.TargetSetting();

        float distance = Vector3.Distance(Player.transform.position, this.transform.position);

        // �����̰� 0���� Ŭ ���
        if(attackDelay >= 0)
        {
            // ������ ����
            attackDelay -= Time.deltaTime;
        }
        
        // ���� �÷��̾� �Ÿ��� �� ���ݻ�Ÿ� ���� �۰ų� �������, �������� �ƴҰ��
        if (attackDelay <= 0 && distance <= attackRange && !IsAttack)
        {
            Attack();
        }
    }

    /** �����Ѵ� */
    private void Attack()
    {
        IsAttack = true;
        StartCoroutine(AttackCO());
    }
    #endregion // �Լ�

    #region �ڷ�ƾ
    /** �⺻ ���� */
    private IEnumerator AttackCO()
    {
        // ���� ���� ����

        yield return null;

        attackDelay = Delay;
        IsAttack = false; 
    }
    #endregion // �ڷ�ƾ
}
