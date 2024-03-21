using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMain : MonoBehaviour
{
    #region 변수
    [SerializeField] private float hp = 0;
    #endregion // 변수

    #region 프로퍼티
    public float HP
    {
        get => hp;
        set => hp = value;
    }
    #endregion // 프로퍼티
}
