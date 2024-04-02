using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SubmachineGunWeapon : Weapon
{
    #region 변수
    private Action coroutin;
    private Vector3 prevTargetPos;
    #endregion // 변수

    #region 함수
    public override void Awake()
    {
        base.Awake();

        coroutin += test;
    }

    public override void WeaponUse()
    {
        base.WeaponUse();
        StartCoroutine(ShotSubmachineGunCO());
    }

    private void test()
    {
        Ammo = MaxAmmo;
        StartCoroutine(ShotSubmachineGunCO());
    }
    #endregion // 함수

    #region 코루틴
    private IEnumerator ShotSubmachineGunCO()
    {
        // 주변에 몹이 있을때 실행
        yield return new WaitUntil(() => PlayerScanner.IsTarget == true);

        while (Ammo > 0)
        {
            Vector3 targetPos = PlayerScanner.NearTarget != null ? PlayerScanner.NearTarget.position : prevTargetPos;
            prevTargetPos = targetPos;
            Vector3 direction = prevTargetPos - this.transform.position;
            direction = direction.normalized;

            GameObject bullet = GameManager.Instance.PoolManager.Get(0);

            bullet.transform.position = this.transform.position;
            bullet.GetComponent<PlayerAttack>().Init(Damage, Penetrate, direction, bulletVelocity);
            bullet.transform.rotation = Quaternion.FromToRotation(Vector3.up, direction);

            Ammo--;
            yield return new WaitForSeconds(Rate);
        }

        StartCoroutine(CoolDownCO(ReloadTime, coroutin));
        // TODO : 비활성화로 관리
    }
    #endregion // 코루틴
}
