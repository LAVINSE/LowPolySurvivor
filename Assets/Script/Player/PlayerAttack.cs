using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    #region ����
    protected Rigidbody rigid;
    #endregion // ����

    #region ������Ƽ
    public int Penetrate { get; set; }
    public float AttackDamage { get; set; }
    #endregion // ������Ƽ

    #region �Լ�
    /** �ʱ�ȭ */
    protected virtual void Awake()
    {
        rigid = GetComponent<Rigidbody>();
    }


    /** �������� ��� (Ʈ����) */
    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            Enemy enemy = other.gameObject.GetComponent<Enemy>();

            if(enemy != null)
            {
                enemy.TakeDamage(AttackDamage);

                if (Penetrate == 0)
                {
                    Attack(enemy); 
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

    public virtual void Attack(Enemy enemy) { }

    public virtual void Ground() { }

    /** �⺻���� */
    public void Init(float attackDamage, int Penetrate, Vector3 direction, float rate = 0)
    {
        this.AttackDamage = attackDamage;
        this.Penetrate = Penetrate;

        rigid.velocity = direction * rate;
    }
    #endregion // �Լ�
}
