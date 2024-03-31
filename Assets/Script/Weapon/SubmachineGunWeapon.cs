using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SubmachineGunWeapon : Weapon
{
    #region 변수
    private Action routin;
    #endregion // 변수

    #region 함수
    public override void Awake()
    {
        base.Awake();

        routin += test;
    }

    public override void WeaponUse()
    {
        base.WeaponUse();
        StartCoroutine(ShotSubmachineGunCO());
    }

    private void test()
    {
        Ammo = equipWeaponDataSO.baseAmmo;
        StartCoroutine(ShotSubmachineGunCO());
    }
    #endregion // 함수

    #region 코루틴
    private IEnumerator ShotSubmachineGunCO()
    {
        // 주변에 몹이 있을때 실행
        yield return new WaitUntil(() => PlayerScanner.targets != null);

        while (Ammo > 0)
        {
            GameObject bullet = GameManager.Instance.PoolManager.Get(0);

            // TODO : 플레이어 방향에 따라 수정 또는 몬스터 위치로 자동 추적
            bullet.GetComponent<Rigidbody>().AddForce(Vector3.forward * 5f, ForceMode.Impulse);
            Ammo--;
            yield return new WaitForSeconds(Rate);
        }

        StartCoroutine(CoolDownCO(ReloadTime, routin));
        // TODO : 비활성화로 관리
    }
    #endregion // 코루틴
}
