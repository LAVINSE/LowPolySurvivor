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
            
            yield return new WaitForSeconds(Rate);
        }

        StartCoroutine(CoolDownCO(ReloadTime, coroutin));
        // TODO : ��Ȱ��ȭ�� ����
    }
    #endregion // �ڷ�ƾ
}
