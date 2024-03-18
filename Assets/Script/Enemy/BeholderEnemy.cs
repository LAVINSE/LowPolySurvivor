using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeholderEnemy : Enemy
{
    #region 변수
    [Header("=====> Beholder 변수 <=====")]
    [SerializeField] private float attackDamage = 0;
    [SerializeField] private float attackRange = 0;
    [SerializeField] private float attackDelay = 0;

    private float Delay = 0;
    #endregion // 변수

    #region 함수
    private void Awake()
    {
        Delay = attackDelay;
    }

    /** 플레이어와 거리를 체크하고 공격한다 */
    public override void TargetSetting()
    {
        base.TargetSetting();

        float distance = Vector3.Distance(Player.transform.position, this.transform.position);

        // 딜레이가 0보다 클 경우
        if(attackDelay >= 0)
        {
            // 딜레이 감소
            attackDelay -= Time.deltaTime;
        }
        
        // 적과 플레이어 거리가 적 공격사거리 보다 작거나 같을경우, 공격중이 아닐경우
        if (attackDelay <= 0 && distance <= attackRange && !IsAttack)
        {
            Attack();
        }
    }

    /** 공격한다 */
    private void Attack()
    {
        IsAttack = true;
        StartCoroutine(AttackCO());
    }
    #endregion // 함수

    #region 코루틴
    /** 기본 공격 */
    private IEnumerator AttackCO()
    {
        // 공격 구현 예정

        yield return null;

        attackDelay = Delay;
        IsAttack = false; 
    }
    #endregion // 코루틴
}
