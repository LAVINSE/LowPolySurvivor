using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    #region 변수
    [Header("=====> Enemy 변수 <=====")]
    [SerializeField] protected float moveSpeed = 0;
    [SerializeField] protected float maxHp = 0;
    [SerializeField] protected float attackRange = 0;
    [SerializeField] protected float attackDelay = 0;

    protected float Delay = 0;
    #endregion // 변수

    #region 프로퍼티
    public bool IsTracking { get; set; } = false;
    public bool IsAttack { get; set; } = false;
    public bool IsDie { get; set; } = false;

    public PlayerMain Player;
    #endregion // 프로퍼티

    #region 함수
    /** 초기화 */
    private void Start()
    {
        Delay = attackDelay;
    }

    /** 데미지를 준다 */
    protected void TakeDamage()
    {
        // Do something
    }

    /** 플레이어와 거리를 체크하고 공격한다 */
    public void TargetSetting()
    {
        float distance = Vector3.Distance(Player.transform.position, this.transform.position);

        // 딜레이가 0보다 클 경우
        if (attackDelay >= 0)
        {
            // 딜레이 감소
            attackDelay -= Time.deltaTime;
            Debug.Log(" 시간 감소 ");
        }

        // 적과 플레이어 거리가 적 공격사거리 보다 작거나 같을경우, 공격중이 아닐경우
        if (attackDelay <= 0 && distance <= attackRange && !IsAttack)
        {
            Debug.Log(" 공격 시작 ");
            // 공격한다 
            Attack();
        }
    }

    /** 공격한다 */
    public virtual void Attack()
    {

    }
    #endregion // 함수
}
