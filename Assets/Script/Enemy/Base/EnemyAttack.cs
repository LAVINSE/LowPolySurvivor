using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
    #region ����
    private float attackDamage = 0;
    #endregion // ����

    #region �Լ�
    /** �������� ��� (Ʈ����) */ 
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            PlayerMain player = other.gameObject.GetComponent<PlayerMain>();
            player.TakeDamage(attackDamage);
        }
    }

    /** �⺻ ���� */
    public void Init(float damage)
    {
        attackDamage = damage;
    }
    #endregion // �Լ�
}
