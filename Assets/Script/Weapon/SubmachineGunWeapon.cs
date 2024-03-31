using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SubmachineGunWeapon : Weapon
{
    #region ����
    private Action routin;
    #endregion // ����

    #region �Լ�
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
    #endregion // �Լ�

    #region �ڷ�ƾ
    private IEnumerator ShotSubmachineGunCO()
    {
        // �ֺ��� ���� ������ ����
        yield return new WaitUntil(() => PlayerScanner.targets != null);

        while (Ammo > 0)
        {
            GameObject bullet = GameManager.Instance.PoolManager.Get(0);

            // TODO : �÷��̾� ���⿡ ���� ���� �Ǵ� ���� ��ġ�� �ڵ� ����
            bullet.GetComponent<Rigidbody>().AddForce(Vector3.forward * 5f, ForceMode.Impulse);
            Ammo--;
            yield return new WaitForSeconds(Rate);
        }

        StartCoroutine(CoolDownCO(ReloadTime, routin));
        // TODO : ��Ȱ��ȭ�� ����
    }
    #endregion // �ڷ�ƾ
}
