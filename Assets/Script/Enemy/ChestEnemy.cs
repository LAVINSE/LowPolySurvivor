using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChestEnemy : Enemy
{
    #region ����
    [Header("=====> Chest ���� <=====")]
    [SerializeField] private BoxCollider attackCollider = null; // ���� �ݶ��̴�

    private Action complete; // ������ �Ϸ� �ƴ���, ��������Ʈ
    private bool isBasicAttack = false; // �⺻ ���� Ȯ��

    private BoxCollider boxCollider; // �ݶ��̴�
    #endregion // ����

    #region �Լ�
    /** �ʱ�ȭ */
    public override void Awake()
    {
        base.Awake();
        boxCollider = GetComponent<BoxCollider>();
        attackCollider.enabled = false;

        complete = OnCompleteBasicAttack;
    }

    /** �����Ѵ� */
    public override void Attack()
    {
        base.Attack();

        if (isBasicAttack == false)
        {
            Debug.Log("������");
            StartCoroutine(BasicAttackCO());
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

        Debug.Log(" ���� ");

        // TODO : ��Ȱ��ȭ ó��, �׽�Ʈ������ ���� ó����
        Destroy(this.gameObject);
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
        isBasicAttack = true;
        IsAttack = true;
        navMeshAgent.isStopped = true;

        attackCollider.GetComponent<EnemyAttack>().Init(attackdamage);

        animator.SetTrigger("attackTrigger");

        yield return new WaitForSeconds(0.1f);
        attackCollider.enabled = true;
        yield return new WaitForSeconds(0.2f);
        attackCollider.enabled = false;
        yield return new WaitForSeconds(0.1f);

        // ��Ÿ�� ����
        StartCoroutine(CoolDownCO(attackDelay, complete));

        navMeshAgent.isStopped = false;
        IsAttack = false;
    }
    #endregion // �ڷ�ƾ
}