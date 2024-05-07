using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventRainAttack : EventAttack
{
    #region ����
    [SerializeField] private float attackDamage;
    
    private WaitForSeconds seconds = new WaitForSeconds(3f);
    #endregion // ����

    #region �Լ�
    protected override void Attack(PlayerMain playerMain)
    {
        base.Attack(playerMain);
        StartCoroutine(RainAttackCO(playerMain));
    }
    #endregion �Լ�

    #region �ڷ�ƾ
    private IEnumerator RainAttackCO(PlayerMain playerMain)
    {
        while(isAttack == true)
        {
            playerMain.TakeDamage(attackDamage);
            yield return seconds;
        }
    }
    #endregion // �ڷ�ƾ
}
