using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatusEffectRain : StatusEffect
{
    #region ����
    private float attackDamage;
    private Coroutine damageCoroutine;
    #endregion // ����

    #region ������
    public StatusEffectRain(float duration, float delay, PlayerMain playerMain, float attackDamage) : base(duration, delay, playerMain)
    {
        this.attackDamage = attackDamage;
    }
    #endregion // ������

    #region �Լ�
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

    /** �������� �ش� */
    private void Damage(float damage)
    {
        playerMain.TakeDamage(damage);
    }
    #endregion // �Լ�

    #region �ڷ�ƾ
    private IEnumerator RainAttackCO()
    {
        while(duration > 0)
        {
            Damage(attackDamage);

            yield return new WaitForSeconds(delay);

            duration -= 1f;
        }
    }
    #endregion // �ڷ�ƾ
}
