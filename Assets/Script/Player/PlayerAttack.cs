using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    #region ����
    protected int Penetrate;

    protected float attackRange;
    protected float AttackDamage;

    protected Rigidbody rigid;
    protected PlayerMain playerMain;
    #endregion // ����

    #region ������Ƽ
    #endregion // ������Ƽ

    #region �Լ�
    /** �ʱ�ȭ */
    protected virtual void Awake()
    {
        rigid = GetComponent<Rigidbody>();
    }

    /** �ʱ�ȭ */
    protected virtual void Start()
    {

    }

    /** �ʱ�ȭ */
    protected virtual void OnEnable()
    {

    }

    /** �ʱ�ȭ */
    protected virtual void OnDisable()
    {
        StopAllCoroutines();
    }

    /** �������� ��� (Ʈ����) */
    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            Enemy enemy = other.gameObject.GetComponent<Enemy>();

            if(enemy != null)
            {
                Attack(enemy);

                if (Penetrate == 0)
                {
                    rigid.velocity = Vector3.zero;
                    this.gameObject.SetActive(false);
                }

                if (Penetrate != -1)
                {
                    Penetrate--;
                }
            }
        }

        if (other.gameObject.CompareTag("Ground"))
        {
            Ground();
        }
    }

    /** ���� */
    public virtual void Attack(Enemy enemy) { }

    /** Ground �±׿� �浹 ���� ��� */
    public virtual void Ground() { }

    /** �⺻���� */
    public void Init(float attackDamage, int Penetrate, Vector3 direction, float rate = 0,
        float attackRange = -1, PlayerMain playerMain = null)
    {
        this.AttackDamage = attackDamage;
        this.Penetrate = Penetrate;
        this.attackRange = attackRange;
        this.playerMain = playerMain;

        rigid.velocity = direction * rate;
    }
    #endregion // �Լ�
}
