using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeholderEnemy : Enemy
{
    #region 변수
    [Header("=====> Beholder 변수 <=====")]
    [SerializeField] private BoxCollider attackCollider = null;

    #endregion // 변수

    #region 함수
    /** 초기화 */
    public override void Awake()
    {
        base.Awake();

        attackCollider.enabled = false;
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
        animator.SetTrigger("attackTrigger");

        yield return new WaitForSeconds(0.3f);
        attackCollider.enabled = true;
        yield return new WaitForSeconds(0.2f);
        attackCollider.enabled = false;
        yield return new WaitForSeconds(0.1f);

        attackDelay = Delay;
        IsAttack = false; 
    }
    #endregion // 코루틴
}
