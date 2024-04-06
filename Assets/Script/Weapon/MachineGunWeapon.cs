using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MachineGunWeapon : Weapon
{
    #region ����
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
        StartCoroutine(MachineGunCO());
    }

    private void test()
    {
        Ammo = MaxAmmo;
        StartCoroutine(MachineGunCO());
    }
    #endregion // �Լ�

    #region �ڷ�ƾ
    private IEnumerator MachineGunCO()
    {
        // �ֺ��� ���� ������ ����
        yield return new WaitUntil(() => PlayerScanner.IsTargetForward == true);

        while (Ammo > 0)
        {
            if (PlayerScanner.ForwardNearTarget == null) { break; }

            Transform target = PlayerScanner.ForwardNearTarget;

            Vector3 targetPos = target.position;
            Vector3 direction = targetPos - this.transform.position;
            direction = direction.normalized;

            // ����� �Ѿ�
            GameObject bullet = GameManager.Instance.PoolManager.GetBullet((int)BulletType.MachineGunBullet, this.transform.position);

            bullet.GetComponent<PlayerAttack>().Init(Damage, Penetrate, direction, bulletVelocity);
            bullet.transform.rotation = Quaternion.FromToRotation(Vector3.up, direction);

            Ammo--;

            yield return new WaitForSeconds(Rate);
        }

        StartCoroutine(CoolDownCO(ReloadTime, coroutin));
        // TODO : ��Ȱ��ȭ�� ����
    }
    #endregion // �ڷ�ƾ
}
