using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    #region 변수
    [Header("=====> Enemy 변수 <=====")]
    [SerializeField] private float moveSpeed = 0;
    [SerializeField] private float maxHp = 0;
    #endregion // 변수

    #region 프로퍼티
    public bool IsTracking { get; set; } = false;
    public bool IsAttack { get; set; } = false;
    public bool IsDie { get; set; } = false;

    public PlayerMain Player { get; set; }
    #endregion // 프로퍼티

    #region 함수
    /** 초기화 */
    private void Awake()
    {
        
    }

    /** 데미지를 준다 */
    protected void TakeDamage()
    {
        // Do something
    }

    /** 플레이어와 거리를 체크하고 공격한다 */
    public virtual void TargetSetting()
    {
        // Do something
    }
    #endregion // 함수
}
