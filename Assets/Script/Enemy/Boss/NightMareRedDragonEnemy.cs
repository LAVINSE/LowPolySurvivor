using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class NightMareRedDragonEnemy : Enemy
{
    #region 변수
    [Header("=====> Turtle 변수 <=====")]
    [SerializeField] private BoxCollider clawAttackCollider = null; // 기본 공격 콜라이더
    [SerializeField] private BoxCollider jumpAttackCollider = null; // 돌진 공격 콜라이더

    private Action complete; // 공격이 완료 됐는지, 델리게이트
    private bool isBasicAttack = false; // 기본 공격 확인

    private BoxCollider boxCollider; // 콜라이더
    #endregion // 변수

    #region 함수
    public override void OnDrawGizmos()
    {
        base.OnDrawGizmos();
    }

    /** 초기화 */
    public override void Awake()
    {
        base.Awake();
        boxCollider = GetComponent<BoxCollider>();
        clawAttackCollider.enabled = false;
        jumpAttackCollider.enabled = false;

        complete = OnCompleteBasicAttack;
    }

    /** 공격한다 */
    public override void Attack(RaycastHit hit)
    {
        base.Attack(hit);

        if (isBasicAttack == false)
        {
            isBasicAttack = true;
            IsAttack = true;

            int random = UnityEngine.Random.Range(1, 101);

            if (random <= 10) // 10%
            {
                // 자버프
                StartCoroutine(ScreamBuffCO());
            }
            else if (random <= 35) // 25%
            {
                // 점프 공격
                StartCoroutine(JumpAttackCO());
            }
            else // 65 %
            {
                // 기본 공격
                StartCoroutine(ClawAttackCO());
            }
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
        boxCollider.enabled = false;

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
    /** 각종 버프를 사용한다 */
    private IEnumerator ScreamBuffCO()
    {
        navMeshAgent.isStopped = true;

        Animator.SetTrigger("screamBuffTrigger");

        yield return null;

        navMeshAgent.isStopped = false;
        IsAttack = false;
    }

    /** 점프 공격 */
    private IEnumerator JumpAttackCO()
    {
        navMeshAgent.isStopped = true;

        jumpAttackCollider.GetComponent<EnemyAttack>().Init(attackdamage);

        Animator.SetTrigger("jumpAttackTrigger");

        yield return null;

        // 쿨타임 시작
        StartCoroutine(CoolDownCO(attackDelay, complete));

        navMeshAgent.isStopped = false;
        IsAttack = false;
    }

    /** 기본 공격 */
    private IEnumerator ClawAttackCO()
    {
        navMeshAgent.isStopped = true;

        clawAttackCollider.GetComponent<EnemyAttack>().Init(attackdamage);

        Animator.SetTrigger("clawAttackTrigger");

        yield return null;

        // 쿨타임 시작
        StartCoroutine(CoolDownCO(attackDelay, complete));

        navMeshAgent.isStopped = false;
        IsAttack = false;
    }
    #endregion // 코루틴
}
