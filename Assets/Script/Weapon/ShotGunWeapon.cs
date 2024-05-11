using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShotGunWeapon : Weapon
{
    #region 변수
    [SerializeField] ShotGunDataSO shotGunDataSO;
    [SerializeField] private float shotAngle = 30f;
    [SerializeField] private float knockBackPower = 2f;

    private Action coroutin;
    #endregion // 변수

    #region 함수
    public override void Awake()
    {
        base.Awake();
        coroutin += test;
    }

    public override void Init()
    {
        base.Init();
        shotAngle = shotGunDataSO.shotAngle;
        knockBackPower = shotGunDataSO.knockBackPower;
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

    private void CreateBullet(Quaternion rotation)
    {
        // 샷건 총알
        GameObject bullet = GameManager.Instance.PoolManager.GetBullet((int)PoolBulletType.ShotGunBullet, this.transform.position);

        bullet.transform.rotation = rotation;

        Vector3 directionPos = bullet.transform.forward;
        directionPos.y = 0;

        // 해당 위치로 부채꼴 공격
        bullet.GetComponent<PlayerAttack>().Init(Damage, Penetrate, directionPos, bulletVelocity);
        bullet.GetComponent<ShotGunBullet>().InitShotGun(knockBackPower);
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
                direction = direction.normalized;

                // 총알 방향을 부채꼴 방향으로 회전 (왼쪽 시작 각도 계산, 발사체 수 만큼 각도 증가)
                float currentAngle = -shotAngle / 2f + i * angleStep;
                Quaternion rotation = Quaternion.LookRotation(direction) * Quaternion.Euler(0, currentAngle, 0);

                // 총알 생성
                CreateBullet(rotation);

                AudioManager.Inst.PlaySFX("GunSoundSFX_1");
                Ammo--;
            }

            yield return new WaitForSeconds(Rate);
        }

        StartCoroutine(CoolDownCO(ReloadTime, coroutin, skillImage));
    }
    #endregion // 코루틴
}
