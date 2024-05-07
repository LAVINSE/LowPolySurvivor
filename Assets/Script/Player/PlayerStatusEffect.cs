using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStatusEffect : MonoBehaviour
{
    #region ����
    private List<StatusEffect> activeEffectList = new List<StatusEffect>();
    #endregion // ����

    #region �Լ�
    public void ApplyBuff(StatusEffect effect)
    {
        effect.ApplyEffect();
        activeEffectList.Add(effect);
        StartCoroutine(RemoveEffectDuration(effect));
    }

    public void RemoveAllEffect()
    {
        foreach(StatusEffect effect in activeEffectList)
        {
            effect.RemoveEffect();
        }

        activeEffectList.Clear();
    }
    #endregion // �Լ�

    #region �ڷ�ƾ
    private IEnumerator RemoveEffectDuration(StatusEffect effect)
    {
        yield return new WaitForSeconds(effect.duration);
        effect.RemoveEffect();
        activeEffectList.Remove(effect);
    }
    #endregion // �ڷ�ƾ
}
