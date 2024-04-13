using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChestEnemy : Enemy
{
    #region 변수
    [Header("=====> Chest 변수 <=====")]
    [SerializeField] private BoxCollider attackCollider = null; // 공격 콜라이더

    private Action complete; // 공격이 완료 됐는지, 델리게이트
    private bool isBasicAttack = false; // 기본 공격 확인

    private BoxCollider boxCollider; // 콜라이더
    #endregion // 변수

    #region 함수
    /** 초기화 */
    public override void Awake()
    {
        base.Awake();
        boxCollider = GetComponent<BoxCollider>();
        attackCollider.enabled = false;

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
            navMeshAgent.isStopped = true;
            Animator.SetBool("isWalk", false);

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
    /** 기본 공격 */
    private IEnumerator BasicAttackCO()
    {
        attackCollider.GetComponent<EnemyAttack>().Init(attackDamage);

        Animator.SetTrigger("attackTrigger");

        yield return new WaitForSeconds(0.1f);
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
