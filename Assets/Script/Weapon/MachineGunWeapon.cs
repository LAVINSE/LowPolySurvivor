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
            int minCount = Mathf.Min(WeaponCount, PlayerScanner.ForwardNearTargetArray.Length - 1);

            for(int i = 0; i <= minCount; i++)
            {
                int count = i;

                if (PlayerScanner.ForwardNearTargetArray.Length - 1 < i)
                {
                    count = PlayerScanner.ForwardNearTargetArray.Length - 1;
                }

                if (PlayerScanner.ForwardNearTargetArray[count] == null) { break; }

                Vector3 targetPos = PlayerScanner.ForwardNearTargetArray[count].position;
                Vector3 direction = targetPos - this.transform.position;
                direction = direction.normalized;
                direction.y = 0;

                // 기관총 총알
                GameObject bullet = GameManager.Instance.PoolManager.GetBullet((int)BulletType.MachineGunBullet, this.transform.position);

                bullet.GetComponent<PlayerAttack>().Init(Damage, Penetrate, direction, bulletVelocity);
                bullet.transform.rotation = Quaternion.FromToRotation(Vector3.left, direction);

                Ammo--;
            }

            yield return new WaitForSeconds(Rate);
        }

        StartCoroutine(CoolDownCO(ReloadTime, coroutin));
    }
    #endregion // 코루틴
}
