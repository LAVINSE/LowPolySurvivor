using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;

public class NightMareRedDragonEnemy : Enemy
{
    #region ����
    [Header("=====> Turtle ���� <=====")]
    [SerializeField] private BoxCollider clawAttackCollider = null; // �⺻ ���� �ݶ��̴�
    [SerializeField] private BoxCollider jumpAttackCollider = null; // ���� ���� �ݶ��̴�
    [SerializeField] private SphereCollider screamAttackCollider = null; // ���� ���� �ݶ��̴�

    private Action complete; // ������ �Ϸ� �ƴ���, ��������Ʈ
    private bool isBasicAttack = false; // �⺻ ���� Ȯ��

    private BoxCollider boxCollider; // �ݶ��̴�
    private int clearCount;
    #endregion // ����

    #region �Լ�
    public override void OnDrawGizmos()
    {
        base.OnDrawGizmos();
    }

    /** �ʱ�ȭ */
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

    /** �����Ѵ� */
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

    /** �������� �޴´� */
    public override void TakeDamage(float damage, float knockBackPower = 0, bool isKnockBack = false)
    {
        base.TakeDamage(damage, knockBackPower, isKnockBack);

        GameManager.Instance.InGameUI.BossHpBarUpdate(maxHp, CurrentHp);
    }

    /** ���� ���� */
    public override void Die()
    {
        base.Die();

        AudioManager.Inst.PlaySFX("BossDieSFX");

        // TODO : �߷� X, �ݶ��̴� isTrigger ���� �����ؾߵ� 
        rigid.useGravity = false;
        boxCollider.enabled = false;

        // ������ ���
        InstantiateDropItem(this.transform.position);

        clearCount++;
        PlayerPrefs.SetInt("ClearStage", clearCount);
        Invoke("ChangeStarMenu", 3f);

        // TODO : ��Ȱ��ȭ ó��, �׽�Ʈ������ ���� ó����
        this.gameObject.SetActive(false);
    }

    /** �⺻������ �Ϸ�Ǿ��� ��� */
    private void OnCompleteBasicAttack()
    {
        isBasicAttack = false;
    }

    private void ChangeStarMenu()
    {
        SceneManager.LoadScene("StartScene");
    }
    #endregion // �Լ�

    #region �ڷ�ƾ
    /** ��� ���� */
    private IEnumerator ScreamAttackCO()
    {
        screamAttackCollider.GetComponent<EnemyAttack>().Init(attackDamage);

        Animator.SetTrigger("screamTrigger");

        yield return new WaitForSeconds(1.9f);
        screamAttackCollider.enabled = true;
        yield return new WaitForSeconds(0.4f);
        screamAttackCollider.enabled = false;
        yield return new WaitForSeconds(0.1f);

        // ��Ÿ�� ����
        StartCoroutine(CoolDownCO(attackDelay, complete));

        navMeshAgent.isStopped = false;
        IsAttack = false;
    }

    /** ���� ���� */
    private IEnumerator JumpAttackCO()
    {
        jumpAttackCollider.GetComponent<EnemyAttack>().Init(attackDamage);

        Animator.SetTrigger("jumpAttackTrigger");

        yield return new WaitForSeconds(1.3f);
        jumpAttackCollider.enabled = true;
        yield return new WaitForSeconds(0.8f);
        jumpAttackCollider.enabled = false;

        yield return new WaitForSeconds(0.1f);

        // ��Ÿ�� ����
        StartCoroutine(CoolDownCO(attackDelay, complete));

        navMeshAgent.isStopped = false;
        IsAttack = false;
    }

    /** �⺻ ���� */
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

        // ��Ÿ�� ����
        StartCoroutine(CoolDownCO(attackDelay, complete));

        navMeshAgent.isStopped = false;
        IsAttack = false;
    }
    #endregion // �ڷ�ƾ
}
