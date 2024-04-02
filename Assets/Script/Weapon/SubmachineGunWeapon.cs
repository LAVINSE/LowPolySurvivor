using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SubmachineGunWeapon : Weapon
{
    #region ����
    private Action coroutin;
    private Vector3 prevTargetPos;
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
        StartCoroutine(ShotSubmachineGunCO());
    }

    private void test()
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
            Vector3 targetPos = PlayerScanner.NearTarget != null ? PlayerScanner.NearTarget.position : prevTargetPos;
            prevTargetPos = targetPos;
            Vector3 direction = prevTargetPos - this.transform.position;
            direction = direction.normalized;

            GameObject bullet = GameManager.Instance.PoolManager.Get(0);

            bullet.transform.position = this.transform.position;
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
