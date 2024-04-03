using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeholderEnemy : Enemy
{
    #region ����
    [Header("=====> Beholder ���� <=====")]
    [SerializeField] private BoxCollider attackCollider = null;

    private BoxCollider boxCollider;
    #endregion // ����

    #region �Լ�
    /** �ʱ�ȭ */
    public override void Awake()
    {
        base.Awake();
        boxCollider = GetComponent<BoxCollider>();
        attackCollider.enabled = false;
    }

    /** �����Ѵ� */
    public override void Attack()
    {
        base.Attack();

        IsAttack = true;
        StartCoroutine(AttackCO());
        Debug.Log(" ���� ����  ");
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
    #endregion // �Լ�

    #region �ڷ�ƾ
    /** �⺻ ���� */
    private IEnumerator AttackCO()
    {
        animator.SetTrigger("attackTrigger");

        yield return new WaitForSeconds(0.3f);
        attackCollider.enabled = true;
        yield return new WaitForSeconds(0.2f);
        attackCollider.enabled = false;
        yield return new WaitForSeconds(0.1f);

        attackDelay = Delay;
        IsAttack = false; 
    }
    #endregion // �ڷ�ƾ
}
