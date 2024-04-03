using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeholderEnemy : Enemy
{
    #region 변수
    [Header("=====> Beholder 변수 <=====")]
    [SerializeField] private BoxCollider attackCollider = null;

    private BoxCollider boxCollider;
    #endregion // 변수

    #region 함수
    /** 초기화 */
    public override void Awake()
    {
        base.Awake();
        boxCollider = GetComponent<BoxCollider>();
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

    /** 데미지를 받는다 */
    public override void TakeDamage(float damage)
    {
        base.TakeDamage(damage);
    }

    /** 몬스터 죽음 */
    public override void Die()
    {
        base.Die();

        // TODO : 중력 X, 콜라이더 isTrigger 해제 설정해야됨 
        rigid.useGravity = false;
        boxCollider.enabled = false;

        // 아이템 드랍
        InstantiateDropItem(this.transform.position);

        Debug.Log(" 죽음 ");

        // TODO : 비활성화 처리, 테스트용으로 삭제 처리함
        Destroy(this.gameObject);
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
