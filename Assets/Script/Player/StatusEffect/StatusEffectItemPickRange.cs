using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatusEffectItemPickRange : StatusEffect
{
    #region ����
    private float itemPickRangeIncrease;
    #endregion // ����

    #region ������
    public StatusEffectItemPickRange(float duration, float delay, PlayerMain playerMain, float itemPickRangeIncrease)
        : base(duration, delay, playerMain)
    {
        this.itemPickRangeIncrease = itemPickRangeIncrease;
    }
    #endregion // ������

    #region �Լ�
    public override void ApplyEffect()
    {
        playerMain.ItemPickRange(itemPickRangeIncrease);
    }

    public override void RemoveEffect()
    {
        playerMain.DeItemPickRange(itemPickRangeIncrease);
    }
    #endregion // �Լ�
}
