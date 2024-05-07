using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class StatusEffect : MonoBehaviour
{
    #region ����
    public float duration;
    protected float delay;
    protected PlayerMain playerMain;
    #endregion // ����

    #region ������
    public StatusEffect(float duration, float delay, PlayerMain playerMain)
    {
        this.duration = duration;
        this.playerMain = playerMain;
        this.delay = delay;
    }
    #endregion // ������

    #region �Լ�
    public abstract void ApplyEffect();
    public abstract void RemoveEffect();
    #endregion // �Լ�
}
