using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeholderEnemy : Enemy
{
    #region 변수
    [Header("=====> Beholder 변수 <=====")]
    [SerializeField] private BoxCollider attackCollider = null;

    private Action complete;
    private bool isBasicAttack = false;

    private CapsuleCollider capsuleCollider;
    #endregion // 변수

    #region 함수
    /** 초기화 */
    public override void Awake()
    {
        base.Awake();
        capsuleCollider = GetComponent<CapsuleCollider>();
        attackCollider.enabled = false;

        complete = OnCompleteBasicAttack;
    }

    /** 공격한다 */
    public override void Attack()
    {
        base.Attack();

        if(isBasicAttack == false)
        {
            Debug.Log("공격중");
            StartCoroutine(BasicAttackCO());
        }
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
        capsuleCollider.enabled = false;

        // 아이템 드랍
        InstantiateDropItem(this.transform.position);

        Debug.Log(" 죽음 ");

        // TODO : 비활성화 처리, 테스트용으로 삭제 처리함
        Destroy(this.gameObject);
    }

    /** 기본공격이 완료되었을 경우 */
    private void OnCompleteBasicAttack()
    {
        isBasicAttack = false;
    }
    #endregion // 함수

    #region 코루틴
    /** 기본 공격 */
    private IEnumerator BasicAttackCO()
    {
        isBasicAttack = true;
        IsAttack = true;
        navMeshAgent.isStopped = true;

        attackCollider.GetComponent<EnemyAttack>().Init(attackdamage);

        animator.SetTrigger("attackTrigger");

        attackCollider.enabled = true;
        yield return new WaitForSeconds(0.2f);
        attackCollider.enabled = false;
        yield return new WaitForSeconds(0.1f);

        // 쿨타임 시작
        StartCoroutine(CoolDownCO(attackDelay, complete));

        navMeshAgent.isStopped = false;
        IsAttack = false;
    }
    #endregion // 코루틴
}