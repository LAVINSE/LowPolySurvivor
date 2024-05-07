using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventPoisonAttack : EventAttack
{
    #region ����
    [SerializeField] private float attackDamage = 1f;

    private WaitForSeconds seconds = new WaitForSeconds(1f);
    #endregion // ����

    #region �Լ�
    protected override void Attack(PlayerMain playerMain)
    {
        base.Attack(playerMain);
        StartCoroutine(PoisonAttackCO(playerMain));
    }
    #endregion // �Լ�

    #region �ڷ�ƾ
    private IEnumerator PoisonAttackCO(PlayerMain playerMain)
    {
        while (isAttack == true)
        {
            playerMain.TakeDamage(attackDamage);
            yield return seconds;
        }
    }
    #endregion // �ڷ�ƾ
}
