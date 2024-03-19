using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeholderEnemy : Enemy
{
    #region 변수
    [Header("=====> Beholder 변수 <=====")]
    [SerializeField] private float attackDamage = 0;

    #endregion // 변수

    #region 함수
    /** 초기화 */
    private void Awake()
    {
        
    }

    /** 공격한다 */
    public override void Attack()
    {
        base.Attack();

        IsAttack = true;
        StartCoroutine(AttackCO());
        Debug.Log(" 공격 진입  ");
    }
    #endregion // 함수

    #region 코루틴
    /** 기본 공격 */
    private IEnumerator AttackCO()
    {
        // 공격 구현 예정
        Debug.Log("공격중");

        yield return null;
        yield return new WaitForSeconds(2f);

        Debug.Log(" 공격 종료 ");
        attackDelay = Delay;
        IsAttack = false; 
    }
    #endregion // 코루틴
}
