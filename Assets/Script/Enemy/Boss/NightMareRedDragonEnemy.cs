using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class NightMareRedDragonEnemy : Enemy
{
    #region ����
    [Header("=====> Turtle ���� <=====")]
    [SerializeField] private BoxCollider clawAttackCollider = null; // �⺻ ���� �ݶ��̴�
    [SerializeField] private BoxCollider jumpAttackCollider = null; // ���� ���� �ݶ��̴�

    private Action complete; // ������ �Ϸ� �ƴ���, ��������Ʈ
    private bool isBasicAttack = false; // �⺻ ���� Ȯ��

    private BoxCollider boxCollider; // �ݶ��̴�
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

        complete = OnCompleteBasicAttack;
    }

    /** �����Ѵ� */
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
                // �ڹ���
                StartCoroutine(ScreamBuffCO());
            }
            else if (random <= 35) // 25%
            {
                // ���� ����
                StartCoroutine(JumpAttackCO());
            }
            else // 65 %
            {
                // �⺻ ����
                StartCoroutine(ClawAttackCO());
            }
        }
    }

    /** �������� �޴´� */
    public override void TakeDamage(float damage)
    {
        base.TakeDamage(damage);
    }

    /** ���� ���� */
    public override void Die()
    {
        base.Die();

        // TODO : �߷� X, �ݶ��̴� isTrigger ���� �����ؾߵ� 
        rigid.useGravity = false;
        boxCollider.enabled = false;

        // ������ ���
        InstantiateDropItem(this.transform.position);

        // TODO : ��Ȱ��ȭ ó��, �׽�Ʈ������ ���� ó����
        this.gameObject.SetActive(false);
    }

    /** �⺻������ �Ϸ�Ǿ��� ��� */
    private void OnCompleteBasicAttack()
    {
        isBasicAttack = false;
    }
    #endregion // �Լ�

    #region �ڷ�ƾ
    /** ���� ������ ����Ѵ� */
    private IEnumerator ScreamBuffCO()
    {
        navMeshAgent.isStopped = true;

        Animator.SetTrigger("screamBuffTrigger");

        yield return null;

        navMeshAgent.isStopped = false;
        IsAttack = false;
    }

    /** ���� ���� */
    private IEnumerator JumpAttackCO()
    {
        navMeshAgent.isStopped = true;

        jumpAttackCollider.GetComponent<EnemyAttack>().Init(attackdamage);

        Animator.SetTrigger("jumpAttackTrigger");

        yield return null;

        // ��Ÿ�� ����
        StartCoroutine(CoolDownCO(attackDelay, complete));

        navMeshAgent.isStopped = false;
        IsAttack = false;
    }

    /** �⺻ ���� */
    private IEnumerator ClawAttackCO()
    {
        navMeshAgent.isStopped = true;

        clawAttackCollider.GetComponent<EnemyAttack>().Init(attackdamage);

        Animator.SetTrigger("clawAttackTrigger");

        yield return null;

        // ��Ÿ�� ����
        StartCoroutine(CoolDownCO(attackDelay, complete));

        navMeshAgent.isStopped = false;
        IsAttack = false;
    }
    #endregion // �ڷ�ƾ
}
