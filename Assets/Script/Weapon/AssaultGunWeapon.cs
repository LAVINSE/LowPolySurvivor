using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AssaultGunWeapon : Weapon
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
        StartCoroutine(AssaultGunGunCO());
    }

    private void test()
    {
        Ammo = MaxAmmo;
        StartCoroutine(AssaultGunGunCO());
    }
    #endregion // 함수

    #region 코루틴
    private IEnumerator AssaultGunGunCO()
    {
        // 주변에 몹이 있을때 실행
        yield return new WaitUntil(() => PlayerScanner.IsTarget == true);

        while(Ammo > 0)
        {
            int minCount = Mathf.Min(WeaponCount, PlayerScanner.NearTargetArray.Length - 1);

            for(int i = 0; i <= minCount; i++)
            {
                int count = i;

                if (PlayerScanner.NearTargetArray.Length - 1 < i)
                {
                    count = PlayerScanner.NearTargetArray.Length - 1;
                }

                if (PlayerScanner.NearTargetArray[count] == null) { break; }

                Vector3 targetPos = PlayerScanner.NearTargetArray[count].position;
                Vector3 direction = targetPos - this.transform.position;
                direction = direction.normalized;

                // 돌격소총 총알
                GameObject bullet = GameManager.Instance.PoolManager.GetBullet((int)BulletType.AssaultGunBullet, this.transform.position);

                bullet.GetComponent<PlayerAttack>().Init(Damage, Penetrate, direction, bulletVelocity);
                bullet.transform.rotation = Quaternion.FromToRotation(Vector3.up, direction);

                Ammo--;
            }

           

            yield return new WaitForSeconds(Rate);
        }

        StartCoroutine(CoolDownCO(ReloadTime, coroutin));
    }
    #endregion // 코루틴
}
