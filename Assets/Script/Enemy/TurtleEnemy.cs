using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurtleEnemy : Enemy
{
    #region 변수
    [Header("=====> Turtle 변수 <=====")]
    [SerializeField] private BoxCollider basicAttackCollider = null; // 기본 공격 콜라이더
    [SerializeField] private BoxCollider rushAttackCollider = null; // 돌진 공격 콜라이더
    [SerializeField] private float rushPower = 0f;
    [SerializeField] private float BaiscRange = 0f;

    private Action complete; // 공격이 완료 됐는지, 델리게이트
    private bool isBasicAttack = false; // 기본 공격 확인

    private BoxCollider boxCollider; // 콜라이더
    #endregion // 변수

    #region 함수
    public override void OnDrawGizmos()
    {
        base.OnDrawGizmos();

        Gizmos.color = Color.red;
        Gizmos.DrawRay(this.transform.position, this.transform.forward * BaiscRange);
    }

    /** 초기화 */
    public override void Awake()
    {
        base.Awake();
        boxCollider = GetComponent<BoxCollider>();
        basicAttackCollider.enabled = false;
        rushAttackCollider.enabled = false;

        complete = OnCompleteBasicAttack;
    }

    /** 초기화 */
    protected override void OnEnable()
    {
        base.OnEnable();

        rigid.useGravity = true;
        boxCollider.enabled = true;
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

            float distance = Vector3.Distance(hit.collider.transform.position, this.transform.position);

            if(distance > BaiscRange)
            {
                StartCoroutine(RushAttackCO());
            }
            else
            {
                StartCoroutine(BasicAttackCO());
            }
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
        basicAttackCollider.GetComponent<EnemyAttack>().Init(attackDamage);

        Animator.SetTrigger("BasicAttackTrigger");

        yield return new WaitForSeconds(0.1f);
        basicAttackCollider.enabled = true;
        yield return new WaitForSeconds(0.2f);
        basicAttackCollider.enabled = false;

        yield return new WaitForSeconds(0.1f);

        // 쿨타임 시작
        StartCoroutine(CoolDownCO(attackDelay, complete));

        navMeshAgent.isStopped = false;
        IsAttack = false;
    }

    /** 돌진 공격 */
    private IEnumerator RushAttackCO()
    {
        rushAttackCollider.GetComponent<EnemyAttack>().Init(attackDamage);

        Animator.SetTrigger("RushAttackTrigger");

        yield return new WaitForSeconds(0.1f);
        rigid.AddForce(this.transform.forward * rushPower, ForceMode.Impulse);
        rushAttackCollider.enabled = true;
        yield return new WaitForSeconds(0.4f);
        rigid.velocity = Vector3.zero;
        rushAttackCollider.enabled = false;

        yield return new WaitForSeconds(0.1f);

        // 쿨타임 시작
        StartCoroutine(CoolDownCO(attackDelay, complete));

        navMeshAgent.isStopped = false;
        IsAttack = false;
    }
    #endregion // 코루틴
}
