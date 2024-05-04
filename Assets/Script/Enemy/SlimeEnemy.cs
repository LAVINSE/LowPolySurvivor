using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlimeEnemy : Enemy
{
    #region 변수
    [Header("=====> Slime 변수 <=====")]
    [SerializeField] private SphereCollider attackCollider = null; // 공격 콜라이더

    private Action complete; // 공격이 완료 됐는지, 델리게이트
    private bool isBasicAttack = false; // 기본 공격 확인

    private CapsuleCollider capsuleCollider; // 콜라이더
    #endregion // 변수

    #region 함수
    public override void OnDrawGizmos()
    {
        base.OnDrawGizmos();
        Gizmos.DrawWireSphere(attackCollider.transform.position, attackRange);
    }

    /** 초기화 */
    public override void Awake()
    {
        base.Awake();

        capsuleCollider = GetComponent<CapsuleCollider>();
        attackCollider.enabled = false;

        complete = OnCompleteBasicAttack;
    }

    /** 초기화 */
    protected override void OnEnable()
    {
        base.OnEnable();
        attackCollider.radius = attackRange;

        rigid.useGravity = true;
        capsuleCollider.enabled = true;
    }

    /** 초기화 */
    protected override void OnDisable()
    {
        base.OnDisable();
    }

    /** 공격한다 */
    public override void Attack(RaycastHit hit)
    {
        base.Attack(hit);

        if (isBasicAttack == false)
        {
            isBasicAttack = true;
            IsAttack = true; 
            navMeshAgent.isStopped = true;
            Animator.SetBool("isWalk", false);

            StartCoroutine(BoomAttackCO());
        }
    }

    /** 데미지를 받는다 */
    public override void TakeDamage(float damage, float knockBackPower = 0, bool isKnockBack = false)
    {
        base.TakeDamage(damage, knockBackPower, isKnockBack);
    }

    /** 몬스터 죽음 */
    public override void Die()
    {
        base.Die();

        // TODO : 중력 X, 콜라이더 isTrigger 해제 설정해야됨 
        rigid.useGravity = false;
        capsuleCollider.enabled = false;

        // 아이템 드랍
        InstantiateDropItem(this.transform.position);

        // TODO : 비활성화 처리, 테스트용으로 삭제 처리함
        this.gameObject.SetActive(false);
    }

    /** 기본공격이 완료되었을 경우 */
    private void OnCompleteBasicAttack()
    {
        isBasicAttack = false;
    }
    #endregion // 함수

    #region 코루틴
    /** 자폭 공격 */
    private IEnumerator BoomAttackCO()
    {
        attackCollider.GetComponent<EnemyAttack>().Init(attackDamage);

        Animator.SetTrigger("BoomTrigger");

        yield return new WaitForSeconds(1.8f);
        attackCollider.enabled = true;
        yield return new WaitForSeconds(0.2f);
        attackCollider.enabled = false;

        TakeDamage(CurrentHp);
    }
    #endregion // 코루틴
}
