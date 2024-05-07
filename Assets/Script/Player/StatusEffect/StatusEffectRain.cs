using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatusEffectRain : StatusEffect
{
    #region 변수
    private float attackDamage;
    private Coroutine damageCoroutine;
    #endregion // 변수

    #region 생성자
    public StatusEffectRain(float duration, float delay, PlayerMain playerMain, float attackDamage) : base(duration, delay, playerMain)
    {
        this.attackDamage = attackDamage;
    }
    #endregion // 생성자

    #region 함수
    public override void ApplyEffect()
    {
        damageCoroutine = StartCoroutine(RainAttackCO());
    }

    public override void RemoveEffect()
    {
        if(damageCoroutine != null)
        {
            StopCoroutine(damageCoroutine);
        }
    }

    /** 데미지를 준다 */
    private void Damage(float damage)
    {
        playerMain.TakeDamage(damage);
    }
    #endregion // 함수

    #region 코루틴
    private IEnumerator RainAttackCO()
    {
        while(duration > 0)
        {
            Damage(attackDamage);

            yield return new WaitForSeconds(delay);

            duration -= 1f;
        }
    }
    #endregion // 코루틴
}
