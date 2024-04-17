using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RockGunWeapon : Weapon
{
    #region ����
    private Action coroutin;
    #endregion // ����

    #region �Լ�
    public override void Awake()
    {
        base.Awake();

        coroutin += CompleteReloadCoolDown;
    }

    public override void WeaponUse()
    {
        base.WeaponUse();
        StartCoroutine(RockGunCO());
    }

    private void CompleteReloadCoolDown()
    {
        Ammo = MaxAmmo;
        StartCoroutine(RockGunCO());
    }
    #endregion // �Լ�

    #region �ڷ�ƾ
    private IEnumerator RockGunCO()
    {
        // �ֺ��� ���� ������ ����
        yield return new WaitUntil(() => PlayerScanner.IsTarget == true);

        while (Ammo > 0)
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

                // ����ź �Ѿ�
                GameObject bullet = GameManager.Instance.PoolManager.GetBullet((int)BulletType.RockBullet, this.transform.position);
                bullet.GetComponent<PlayerAttack>().Init(Damage, Penetrate, direction, bulletVelocity);
                Quaternion.FromToRotation(Vector3.up, direction);

                Ammo--;
            }


            yield return new WaitForSeconds(Rate);
        }

        StartCoroutine(CoolDownCO(ReloadTime, coroutin));
    }
    #endregion // �ڷ�ƾ
}