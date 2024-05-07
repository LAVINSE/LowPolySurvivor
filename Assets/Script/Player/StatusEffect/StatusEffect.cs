using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class StatusEffect : MonoBehaviour
{
    #region 변수
    public float duration;
    protected float delay;
    protected PlayerMain playerMain;
    #endregion // 변수

    #region 생성자
    public StatusEffect(float duration, float delay, PlayerMain playerMain)
    {
        this.duration = duration;
        this.playerMain = playerMain;
        this.delay = delay;
    }
    #endregion // 생성자

    #region 함수
    public abstract void ApplyEffect();
    public abstract void RemoveEffect();
    #endregion // 함수
}
