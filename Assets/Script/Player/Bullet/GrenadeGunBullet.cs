using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrenadeGunBullet : PlayerAttack
{
    #region 변수
    [SerializeField] private BoxCollider boxCollider; // 수류탄 콜라이더
    #endregion // 변수

    #region 함수
    public override void Attack(Enemy enemy)
    {
        base.Attack(enemy);

        rigid.velocity = Vector3.zero;
        this.gameObject.SetActive(false);
    }

    public override void Ground()
    {
        base.Ground();

        rigid.velocity = Vector3.zero;
        this.gameObject.SetActive(false);
    }
    #endregion // 함수

    #region 코루틴
    #endregion // 코루틴
}
