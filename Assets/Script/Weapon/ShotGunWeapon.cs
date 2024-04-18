using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShotGunWeapon : Weapon
{
    #region ����
    [SerializeField] ShotGunDataSO shotGunDataSO;
    [SerializeField] private float shotAngle = 30f;
    [SerializeField] private float knockBackPower = 2f;

    private Action coroutin;
    #endregion // ����

    #region �Լ�
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
                direction = direction.normalized;

                // �Ѿ� ������ ��ä�� �������� ȸ�� (���� ���� ���� ���, �߻�ü �� ��ŭ ���� ����)
                float currentAngle = -shotAngle / 2f + i * angleStep;
                Quaternion rotation = Quaternion.LookRotation(direction) * Quaternion.Euler(0, currentAngle, 0);

                // ���� �Ѿ�
                GameObject bullet = GameManager.Instance.PoolManager.GetBullet((int)BulletType.ShotGunBullet, this.transform.position);

                bullet.transform.rotation = rotation;

                Vector3 directionPos = bullet.transform.forward;
                directionPos.y = 0;

                // �ش� ��ġ�� ��ä�� ����
                bullet.GetComponent<PlayerAttack>().Init(Damage, Penetrate, directionPos, bulletVelocity);
                bullet.GetComponent<ShotGunBullet>().InitShotGun(knockBackPower);

                Ammo--;
            }

            yield return new WaitForSeconds(Rate);
        }

        StartCoroutine(CoolDownCO(ReloadTime, coroutin));
        // TODO : ��Ȱ��ȭ�� ����
    }
    #endregion // �ڷ�ƾ
}
