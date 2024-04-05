using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShotGunWeapon : Weapon
{
    #region 변수
    [SerializeField] private float shotAngle = 30f;

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
        StartCoroutine(ShotGunCO());
    }

    private void test()
    {
        Ammo = MaxAmmo;
        StartCoroutine(ShotGunCO());
    }
    #endregion // 함수

    #region 코루틴
    private IEnumerator ShotGunCO()
    {
        // 주변에 몹이 있을때 실행
        yield return new WaitUntil(() => PlayerScanner.IsTarget == true);

        while (Ammo > 0)
        {
            // 총알 간의 각도 간격 계산
            float angleStep = shotAngle / (WeaponCount - 1); 

            for (int i = 0; i < WeaponCount; i++)
            {
                if (PlayerScanner.ForwardNearTarget == null) { break; }

                Vector3 target = PlayerScanner.ForwardNearTarget.position;
                Vector3 direction = target - this.transform.position;

                // 총알 방향을 부채꼴 방향으로 회전 (왼쪽 시작 각도 계산, 발사체 수 만큼 각도 증가)
                float currentAngle = -shotAngle / 2f + i * angleStep;
                Quaternion rotation = Quaternion.LookRotation(direction) * Quaternion.Euler(0, currentAngle, 0);

                // 샷건 총알
                GameObject bullet = GameManager.Instance.PoolManager.GetBullet((int)BulletType.ShotGunBullet);

                bullet.transform.position = this.transform.position;
                bullet.transform.rotation = rotation;

                // 해당 위치로 부채꼴 공격
                bullet.GetComponent<PlayerAttack>().Init(Damage, Penetrate, bullet.transform.forward, bulletVelocity);

                Ammo--;
            }

            yield return new WaitForSeconds(Rate);
        }

        StartCoroutine(CoolDownCO(ReloadTime, coroutin));
        // TODO : 비활성화로 관리
    }
    #endregion // 코루틴
}
