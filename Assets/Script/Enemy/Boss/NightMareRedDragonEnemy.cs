using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;

public class NightMareRedDragonEnemy : Enemy
{
    #region 변수
    [Header("=====> Turtle 변수 <=====")]
    [SerializeField] private BoxCollider clawAttackCollider = null; // 기본 공격 콜라이더
    [SerializeField] private BoxCollider jumpAttackCollider = null; // 돌진 공격 콜라이더
    [SerializeField] private SphereCollider screamAttackCollider = null; // 돌진 공격 콜라이더

    private Action complete; // 공격이 완료 됐는지, 델리게이트
    private bool isBasicAttack = false; // 기본 공격 확인

    private BoxCollider boxCollider; // 콜라이더
    private int clearCount;
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
        screamAttackCollider.enabled = false;

        complete = OnCompleteBasicAttack;

        if (PlayerPrefs.HasKey("ClearStage"))
        {
            clearCount = PlayerPrefs.GetInt("ClearStage");
        }
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

            int random = UnityEngine.Random.Range(1, 101);

            if (random <= 10) // 10%
            {
                StartCoroutine(ScreamAttackCO());
            }
            else if (random <= 35) // 25%
            {
                StartCoroutine(JumpAttackCO());
            }
            else // 65 %
            {
                StartCoroutine(ClawAttackCO());
            }
        }
    }

    /** 데미지를 받는다 */
    public override void TakeDamage(float damage, float knockBackPower = 0, bool isKnockBack = false)
    {
        base.TakeDamage(damage, knockBackPower, isKnockBack);

        GameManager.Instance.InGameUI.BossHpBarUpdate(maxHp, CurrentHp);
    }

    /** 몬스터 죽음 */
    public override void Die()
    {
        base.Die();

        AudioManager.Inst.PlaySFX("BossDieSFX");

        // TODO : 중력 X, 콜라이더 isTrigger 해제 설정해야됨 
        rigid.useGravity = false;
        boxCollider.enabled = false;

        // 아이템 드랍
        InstantiateDropItem(this.transform.position);

        clearCount++;
        PlayerPrefs.SetInt("ClearStage", clearCount);
        Invoke("ChangeStarMenu", 3f);

        // TODO : 비활성화 처리, 테스트용으로 삭제 처리함
        this.gameObject.SetActive(false);
    }

    /** 기본공격이 완료되었을 경우 */
    private void OnCompleteBasicAttack()
    {
        isBasicAttack = false;
    }

    private void ChangeStarMenu()
    {
        SceneManager.LoadScene("StartScene");
    }
    #endregion // 함수

    #region 코루틴
    /** 비명 공격 */
    private IEnumerator ScreamAttackCO()
    {
        screamAttackCollider.GetComponent<EnemyAttack>().Init(attackDamage);

        Animator.SetTrigger("screamTrigger");

        yield return new WaitForSeconds(1.9f);
        screamAttackCollider.enabled = true;
        yield return new WaitForSeconds(0.4f);
        screamAttackCollider.enabled = false;
        yield return new WaitForSeconds(0.1f);

        // 쿨타임 시작
        StartCoroutine(CoolDownCO(attackDelay, complete));

        navMeshAgent.isStopped = false;
        IsAttack = false;
    }

    /** 점프 공격 */
    private IEnumerator JumpAttackCO()
    {
        jumpAttackCollider.GetComponent<EnemyAttack>().Init(attackDamage);

        Animator.SetTrigger("jumpAttackTrigger");

        yield return new WaitForSeconds(1.3f);
        jumpAttackCollider.enabled = true;
        yield return new WaitForSeconds(0.8f);
        jumpAttackCollider.enabled = false;

        yield return new WaitForSeconds(0.1f);

        // 쿨타임 시작
        StartCoroutine(CoolDownCO(attackDelay, complete));

        navMeshAgent.isStopped = false;
        IsAttack = false;
    }

    /** 기본 공격 */
    private IEnumerator ClawAttackCO()
    {
        clawAttackCollider.GetComponent<EnemyAttack>().Init(attackDamage);

        AudioManager.Inst.PlaySFX("BossAttackSFX");

        Animator.SetTrigger("clawAttackTrigger");
        yield return new WaitForSeconds(1.6f);
        clawAttackCollider.enabled = true;
        yield return new WaitForSeconds(0.7f);
        clawAttackCollider.enabled = false;

        yield return new WaitForSeconds(0.1f);

        // 쿨타임 시작
        StartCoroutine(CoolDownCO(attackDelay, complete));

        navMeshAgent.isStopped = false;
        IsAttack = false;
    }
    #endregion // 코루틴
}
