using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
    #region 변수
    private float attackDamage = 0;
    #endregion // 변수

    #region 함수
    /** 접촉했을 경우 (트리거) */ 
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            PlayerMain player = other.gameObject.GetComponent<PlayerMain>();

            if(player != null )
            {
                player.TakeDamage(attackDamage);
            }
        }
    }

    /** 기본 설정 */
    public void Init(float damage)
    {
        attackDamage = damage;
    }
    #endregion // 함수
}
