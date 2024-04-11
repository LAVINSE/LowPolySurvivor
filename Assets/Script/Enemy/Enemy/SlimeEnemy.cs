using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlimeEnemy : Enemy
{
    #region ����
    [Header("=====> Slime ���� <=====")]
    [SerializeField] private SphereCollider attackCollider = null; // ���� �ݶ��̴�

    private Action complete; // ������ �Ϸ� �ƴ���, ��������Ʈ
    private bool isBasicAttack = false; // �⺻ ���� Ȯ��

    private CapsuleCollider capsuleCollider; // �ݶ��̴�
    #endregion // ����

    #region �Լ�
    /** �ʱ�ȭ */
    public override void Awake()
    {
        base.Awake();
        capsuleCollider = GetComponent<CapsuleCollider>();
        attackCollider.enabled = false;

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
        capsuleCollider.enabled = false;

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
        navMeshAgent.isStopped = true;

        attackCollider.GetComponent<EnemyAttack>().Init(attackdamage);

        Animator.SetTrigger("attackTrigger");

        // ���� ����

        yield return new WaitForSeconds(0.1f);
        attackCollider.enabled = true;
        yield return new WaitForSeconds(0.2f);
        attackCollider.enabled = false;
        yield return new WaitForSeconds(0.1f);
    }
    #endregion // �ڷ�ƾ
}
