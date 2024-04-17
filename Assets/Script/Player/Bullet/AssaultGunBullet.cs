using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AssaultGunBullet : PlayerAttack
{
    #region ����

    #endregion // ����

    #region �Լ�
    public override void Attack(Enemy enemy)
    {
        base.Attack(enemy);
        enemy.TakeDamage(AttackDamage);
    }

    public override void Ground()
    {
        base.Ground();

        rigid.velocity = Vector3.zero;
        this.gameObject.SetActive(false);
    }
    #endregion // �Լ�
}