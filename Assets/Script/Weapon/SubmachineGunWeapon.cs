using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SubmachineGunWeapon : Weapon
{
    #region 변수
    private Action coroutin;
    #endregion // 변수

    #region 함수
    public override void Awake()
    {
        base.Awake();

        coroutin += CompleteReloadCoolDown;
    }

    public override void WeaponUse()
    {
        base.WeaponUse();
        StartCoroutine(ShotSubmachineGunCO());
    }

    private void CompleteReloadCoolDown()
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
            int minCount = Mathf.Min(WeaponCount, PlayerScanner.NearTargetArray.Length - 1);

            // TODO : 배열 범위 벗어나지 않게 수정해야됨
            for (int i = 0; i <= minCount; i++)
            {
                int count = i;

                if (PlayerScanner.NearTargetArray.Length - 1 < i)
                {
                    count = PlayerScanner.NearTargetArray.Length - 1;
                }

                Transform target = PlayerScanner.NearTargetArray[count];

                Vector3 targetPos = target.position;
                Vector3 direction = targetPos - this.transform.position;
                direction = direction.normalized;

                // 0 >> 기관단총 총알
                GameObject bullet = GameManager.Instance.PoolManager.Get((int)ObjectType.SubmachineGunBullet);

                bullet.transform.position = this.transform.position;
                bullet.GetComponent<PlayerAttack>().Init(Damage, Penetrate, direction, bulletVelocity);
                bullet.transform.rotation = Quaternion.FromToRotation(Vector3.up, direction);

                Ammo--;
            }

            yield return new WaitForSeconds(Rate);
        }

        StartCoroutine(CoolDownCO(ReloadTime, coroutin));
        // TODO : 비활성화로 관리
    }
    #endregion // 코루틴
}
