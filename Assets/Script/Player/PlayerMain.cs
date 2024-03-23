using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMain : MonoBehaviour
{
    #region 변수
    [Header("=====> 플레이어 정보 <=====")]
    [SerializeField] private float maxHp;
    #endregion // 변수

    #region 프로퍼티
    public float CurrentHp { get; set; }
    #endregion // 프로퍼티

    #region 함수
    /** 초기화 */
    private void Awake()
    {
        // 현재 체력 설정
        CurrentHp = maxHp;
    }
    #endregion // 함수
}
