using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShotGunWeapon : Weapon
{
    #region ����
    [SerializeField] private float shotAngle = 30f;

    private Action coroutin;
    #endregion // ����

    #region �Լ�
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
    #endregion // �Լ�

    #region �ڷ�ƾ
    private IEnumerator ShotGunCO()
    {
        // �ֺ��� ���� ������ ����
        yield return new WaitUntil(() => PlayerScanner.IsTarget == true);

        while (Ammo > 0)
        {
            // �Ѿ� ���� ���� ���� ���
            float angleStep = shotAngle / (WeaponCount - 1); 

            for (int i = 0; i < WeaponCount; i++)
            {
                if (PlayerScanner.ForwardNearTarget == null) { break; }

                Vector3 target = PlayerScanner.ForwardNearTarget.position;
                Vector3 direction = target - this.transform.position;

                // �Ѿ� ������ ��ä�� �������� ȸ�� (���� ���� ���� ���, �߻�ü �� ��ŭ ���� ����)
                float currentAngle = -shotAngle / 2f + i * angleStep;
                Quaternion rotation = Quaternion.LookRotation(direction) * Quaternion.Euler(0, currentAngle, 0);

                // ���� �Ѿ�
                GameObject bullet = GameManager.Instance.PoolManager.GetBullet((int)BulletType.ShotGunBullet);

                bullet.transform.position = this.transform.position;
                bullet.transform.rotation = rotation;

                // �ش� ��ġ�� ��ä�� ����
                bullet.GetComponent<PlayerAttack>().Init(Damage, Penetrate, bullet.transform.forward, bulletVelocity);

                Ammo--;
            }

            yield return new WaitForSeconds(Rate);
        }

        StartCoroutine(CoolDownCO(ReloadTime, coroutin));
        // TODO : ��Ȱ��ȭ�� ����
    }
    #endregion // �ڷ�ƾ
}
