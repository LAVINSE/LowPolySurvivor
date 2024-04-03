using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SubmachineGunWeapon : Weapon
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
        StartCoroutine(ShotSubmachineGunCO());
    }

    private void CompleteReloadCoolDown()
    {
        Ammo = MaxAmmo;
        StartCoroutine(ShotSubmachineGunCO());
    }
    #endregion // �Լ�

    #region �ڷ�ƾ
    private IEnumerator ShotSubmachineGunCO()
    {
        // �ֺ��� ���� ������ ����
        yield return new WaitUntil(() => PlayerScanner.IsTarget == true);

        while (Ammo > 0)
        {
            int minCount = Mathf.Min(WeaponCount, PlayerScanner.NearTargetArray.Length - 1);

            // TODO : �迭 ���� ����� �ʰ� �����ؾߵ�
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

                // 0 >> ������� �Ѿ�
                GameObject bullet = GameManager.Instance.PoolManager.Get((int)ObjectType.SubmachineGunBullet);

                bullet.transform.position = this.transform.position;
                bullet.GetComponent<PlayerAttack>().Init(Damage, Penetrate, direction, bulletVelocity);
                bullet.transform.rotation = Quaternion.FromToRotation(Vector3.up, direction);

                Ammo--;
            }

            yield return new WaitForSeconds(Rate);
        }

        StartCoroutine(CoolDownCO(ReloadTime, coroutin));
        // TODO : ��Ȱ��ȭ�� ����
    }
    #endregion // �ڷ�ƾ
}
