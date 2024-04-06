using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MachineGunWeapon : Weapon
{
    #region 변수
    private Action coroutin;
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
        StartCoroutine(MachineGunCO());
    }

    private void test()
    {
        Ammo = MaxAmmo;
        StartCoroutine(MachineGunCO());
    }
    #endregion // 함수

    #region 코루틴
    private IEnumerator MachineGunCO()
    {
        // 주변에 몹이 있을때 실행
        yield return new WaitUntil(() => PlayerScanner.IsTargetForward == true);

        while (Ammo > 0)
        {
            if (PlayerScanner.ForwardNearTarget == null) { break; }

            Transform target = PlayerScanner.ForwardNearTarget;

            Vector3 targetPos = target.position;
            Vector3 direction = targetPos - this.transform.position;
            direction = direction.normalized;

            // 기관총 총알
            GameObject bullet = GameManager.Instance.PoolManager.GetBullet((int)BulletType.MachineGunBullet, this.transform.position);

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
