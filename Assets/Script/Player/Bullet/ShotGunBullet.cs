using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShotGunBullet : PlayerAttack
{
    #region 변수
    [SerializeField] private float knockBackPower = 2f;
    #endregion // 변수

    #region 함수
    public void InitShotGun(float knockBackPower)
    {
        this.knockBackPower = knockBackPower;
    }

    public override void Attack(Enemy enemy)
    {
        base.Attack(enemy);
        enemy.TakeDamage(AttackDamage, knockBackPower, true);
    }

    public override void Ground()
    {
        base.Ground();

        rigid.velocity = Vector3.zero;
        this.gameObject.SetActive(false);
    }
    #endregion // 함수
}
