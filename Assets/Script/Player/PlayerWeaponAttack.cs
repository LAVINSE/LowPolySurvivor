using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWeaponAttack : MonoBehaviour
{
    #region ����
    private float attackDamage = 0f;
    #endregion // ����

    #region �Լ�
    /** �������� ��� (Ʈ����) */
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            Enemy enemy = other.gameObject.GetComponent<Enemy>();

            if (enemy != null)
            {
                enemy.TakeDamage(attackDamage);
            }
        }
    }

    /** �⺻ ���� */
    public void Init(float attackDamage)
    {
        this.attackDamage = attackDamage;
    }
    #endregion // �Լ�
}
