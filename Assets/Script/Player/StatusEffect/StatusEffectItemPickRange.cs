using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatusEffectItemPickRange : StatusEffect
{
    #region 변수
    private float itemPickRangeIncrease;
    #endregion // 변수

    #region 생성자
    public StatusEffectItemPickRange(float duration, float delay, PlayerMain playerMain, float itemPickRangeIncrease)
        : base(duration, delay, playerMain)
    {
        this.itemPickRangeIncrease = itemPickRangeIncrease;
    }
    #endregion // 생성자

    #region 함수
    public override void ApplyEffect()
    {
        playerMain.ItemPickRange(itemPickRangeIncrease);
    }

    public override void RemoveEffect()
    {
        playerMain.DeItemPickRange(itemPickRangeIncrease);
    }
    #endregion // 함수
}
