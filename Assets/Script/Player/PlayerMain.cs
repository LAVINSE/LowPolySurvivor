using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMain : MonoBehaviour
{
    #region ����
    [SerializeField] private float hp = 0;
    #endregion // ����

    #region ������Ƽ
    public float HP
    {
        get => hp;
        set => hp = value;
    }
    #endregion // ������Ƽ
}
