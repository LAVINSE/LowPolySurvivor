using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurtleEnemy : Enemy
{
    #region ����
    [Header("=====> Turtle ���� <=====")]
    [SerializeField] private BoxCollider basicAttackCollider = null; // �⺻ ���� �ݶ��̴�
    [SerializeField] private BoxCollider rushAttackCollider = null; // ���� ���� �ݶ��̴�
    [SerializeField] private float rushPower = 0f;
    [SerializeField] private float BaiscRange = 0f;

    private Action complete; // ������ �Ϸ� �ƴ���, ��������Ʈ
    private bool isBasicAttack = false; // �⺻ ���� Ȯ��

    private BoxCollider boxCollider; // �ݶ��̴�
    #endregion // ����

    #region �Լ�
    public override void OnDrawGizmos()
    {
        base.OnDrawGizmos();

        Gizmos.color = Color.red;
        Gizmos.DrawRay(this.transform.position, this.transform.forward * BaiscRange);
    }

    /** �ʱ�ȭ */
    public override void Awake()
    {
        base.Awake();
        boxCollider = GetComponent<BoxCollider>();
        basicAttackCollider.enabled = false;
        rushAttackCollider.enabled = false;

        complete = OnCompleteBasicAttack;
    }

    /** �ʱ�ȭ */
    protected override void OnEnable()
    {
        base.OnEnable();

        rigid.useGravity = true;
        boxCollider.enabled = true;
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

    /** �������� �޴´� */
    public override void TakeDamage(float damage, float knockBackPower = 0, bool isKnockBack = false)
    {
        base.TakeDamage(damage, knockBackPower, isKnockBack);
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
    /** �⺻ ���� */
    private IEnumerator BasicAttackCO()
    {
        basicAttackCollider.GetComponent<EnemyAttack>().Init(attackDamage);

        Animator.SetTrigger("BasicAttackTrigger");

        yield return new WaitForSeconds(0.1f);
        basicAttackCollider.enabled = true;
        yield return new WaitForSeconds(0.2f);
        basicAttackCollider.enabled = false;

        yield return new WaitForSeconds(0.1f);

        // ��Ÿ�� ����
        StartCoroutine(CoolDownCO(attackDelay, complete));

        navMeshAgent.isStopped = false;
        IsAttack = false;
    }

    /** ���� ���� */
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

        // ��Ÿ�� ����
        StartCoroutine(CoolDownCO(attackDelay, complete));

        navMeshAgent.isStopped = false;
        IsAttack = false;
    }
    #endregion // �ڷ�ƾ
}
