using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.UI.Image;

public class GrenadeGunWeapon : Weapon
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
        StartCoroutine(GrenadeGunCO());
    }

    private void CompleteReloadCoolDown()
    {
        Ammo = MaxAmmo;
        StartCoroutine(GrenadeGunCO());
    }

    /** 포물선 운동을 시뮬레이션하고 초기 속도를 계산하여 반환 */
    public Vector3 GetVelocity(Vector3 player, Vector3 target, float initialAngle)
    {
        // 중력 가속도 가져오기
        float gravity = Physics.gravity.magnitude;
        // 초기 각도를 라디안으로 변환
        float angle = initialAngle * Mathf.Deg2Rad;

        // 목표물과 발사체의 y 좌표를 제외한 평면 상의 위치 계산
        Vector3 planarTarget = new Vector3(target.x, 0, target.z);
        Vector3 planarPosition = new Vector3(player.x, 0, player.z);

        // 목표물과 발사체 사이의 거리 및 y 좌표의 차이 계산
        float distance = Vector3.Distance(planarTarget, planarPosition);
        float yOffset = player.y - target.y;

        // 초기 속도 계산
        float initialVelocity = (1 / Mathf.Cos(angle)) * Mathf.Sqrt((0.5f * gravity * Mathf.Pow(distance, 2)) / (distance * Mathf.Tan(angle) + yOffset));

        // x, y, z 방향의 속도 계산
        Vector3 velocity = new Vector3(0f, initialVelocity * Mathf.Sin(angle), initialVelocity * Mathf.Cos(angle));

        // 목표물과 발사체 사이의 각도를 계산하여 방향 조정
        float angleBetweenObjects = Vector3.Angle(Vector3.forward, planarTarget - planarPosition) * (target.x > player.x ? 1 : -1);
        Vector3 finalVelocity = Quaternion.AngleAxis(angleBetweenObjects, Vector3.up) * velocity;

        // 최종적인 속도 벡터 반환
        return finalVelocity;
    }
    #endregion // 함수

    #region 코루틴
    private IEnumerator GrenadeGunCO()
    {
        // 주변에 몹이 있을때 실행
        yield return new WaitUntil(() => PlayerScanner.IsTarget == true);

        while (Ammo > 0)
        {
            int minCount = Mathf.Min(WeaponCount, PlayerScanner.NearTargetArray.Length - 1);

            for ( int i = 0; i <= minCount; i++ )
            {
                int count = i;

                if (PlayerScanner.NearTargetArray.Length - 1 < i)
                {
                    count = PlayerScanner.NearTargetArray.Length - 1;
                }

                if (PlayerScanner.NearTargetArray[count] == null) { break; }

                Vector3 targetPos = PlayerScanner.NearTargetArray[count].position;

                // 수류탄 총알
                GameObject bullet = GameManager.Instance.PoolManager.GetBullet((int)PoolBulletType.GrenadeBullet, this.transform.position);
                Vector3 velocitay = GetVelocity(this.transform.position, targetPos, 45f);

                bullet.GetComponent<PlayerAttack>().Init(Damage, Penetrate, velocitay, bulletVelocity);
                AudioManager.Inst.PlaySFX("GunSoundSFX_1");
                Ammo--;
            }
            yield return new WaitForSeconds(Rate);
        }

        StartCoroutine(CoolDownCO(ReloadTime, coroutin, skillImage));
    }
    #endregion // 코루틴
}
