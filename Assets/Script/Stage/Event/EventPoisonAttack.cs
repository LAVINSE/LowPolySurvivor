using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventPoisonAttack : EventAttack
{
    #region 변수
    [SerializeField] private float attackDamage = 1f;

    private WaitForSeconds seconds = new WaitForSeconds(1f);
    #endregion // 변수

    #region 함수
    protected override void Attack(PlayerMain playerMain)
    {
        base.Attack(playerMain);
        StartCoroutine(PoisonAttackCO(playerMain));
    }
    #endregion // 함수

    #region 코루틴
    private IEnumerator PoisonAttackCO(PlayerMain playerMain)
    {
        while (isAttack == true)
        {
            playerMain.TakeDamage(attackDamage);
            yield return seconds;
        }
    }
    #endregion // 코루틴
}
