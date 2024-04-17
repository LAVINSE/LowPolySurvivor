using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWeaponAttack : MonoBehaviour
{
    #region 변수
    private float attackDamage = 0f;
    #endregion // 변수

    #region 함수
    /** 접촉했을 경우 (트리거) */
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

    /** 기본 설정 */
    public void Init(float attackDamage)
    {
        this.attackDamage = attackDamage;
    }
    #endregion // 함수
}
