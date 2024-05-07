using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStatusEffect : MonoBehaviour
{
    #region 변수
    private List<StatusEffect> activeEffectList = new List<StatusEffect>();
    #endregion // 변수

    #region 함수
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
    #endregion // 함수

    #region 코루틴
    private IEnumerator RemoveEffectDuration(StatusEffect effect)
    {
        yield return new WaitForSeconds(effect.duration);
        effect.RemoveEffect();
        activeEffectList.Remove(effect);
    }
    #endregion // 코루틴
}
