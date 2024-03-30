using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    #region ����
    [SerializeField] private float attackDamage = 0;
    #endregion // ����

    #region �Լ�
    /** �������� ��� (Ʈ����) */
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            Enemy enemy = other.gameObject.GetComponent<Enemy>();
            enemy.CurrentHp -= attackDamage;
        }
    }
    #endregion // �Լ�
}
