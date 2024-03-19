using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeholderEnemy : Enemy
{
    #region ����
    [Header("=====> Beholder ���� <=====")]
    [SerializeField] private float attackDamage = 0;

    #endregion // ����

    #region �Լ�
    /** �ʱ�ȭ */
    private void Awake()
    {
        
    }

    /** �����Ѵ� */
    public override void Attack()
    {
        base.Attack();

        IsAttack = true;
        StartCoroutine(AttackCO());
        Debug.Log(" ���� ����  ");
    }
    #endregion // �Լ�

    #region �ڷ�ƾ
    /** �⺻ ���� */
    private IEnumerator AttackCO()
    {
        // ���� ���� ����
        Debug.Log("������");

        yield return null;
        yield return new WaitForSeconds(2f);

        Debug.Log(" ���� ���� ");
        attackDelay = Delay;
        IsAttack = false; 
    }
    #endregion // �ڷ�ƾ
}
