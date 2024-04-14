using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.UI.Image;

public class GrenadeGunWeapon : Weapon
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

    /** ������ ��� �ùķ��̼��ϰ� �ʱ� �ӵ��� ����Ͽ� ��ȯ */
    public Vector3 GetVelocity(Vector3 player, Vector3 target, float initialAngle)
    {
        // �߷� ���ӵ� ��������
        float gravity = Physics.gravity.magnitude;
        // �ʱ� ������ �������� ��ȯ
        float angle = initialAngle * Mathf.Deg2Rad;

        // ��ǥ���� �߻�ü�� y ��ǥ�� ������ ��� ���� ��ġ ���
        Vector3 planarTarget = new Vector3(target.x, 0, target.z);
        Vector3 planarPosition = new Vector3(player.x, 0, player.z);

        // ��ǥ���� �߻�ü ������ �Ÿ� �� y ��ǥ�� ���� ���
        float distance = Vector3.Distance(planarTarget, planarPosition);
        float yOffset = player.y - target.y;

        // �ʱ� �ӵ� ���
        float initialVelocity = (1 / Mathf.Cos(angle)) * Mathf.Sqrt((0.5f * gravity * Mathf.Pow(distance, 2)) / (distance * Mathf.Tan(angle) + yOffset));

        // x, y, z ������ �ӵ� ���
        Vector3 velocity = new Vector3(0f, initialVelocity * Mathf.Sin(angle), initialVelocity * Mathf.Cos(angle));

        // ��ǥ���� �߻�ü ������ ������ ����Ͽ� ���� ����
        float angleBetweenObjects = Vector3.Angle(Vector3.forward, planarTarget - planarPosition) * (target.x > player.x ? 1 : -1);
        Vector3 finalVelocity = Quaternion.AngleAxis(angleBetweenObjects, Vector3.up) * velocity;

        // �������� �ӵ� ���� ��ȯ
        return finalVelocity;
    }
    #endregion // �Լ�

    #region �ڷ�ƾ
    private IEnumerator ShotSubmachineGunCO()
    {
        // �ֺ��� ���� ������ ����
        yield return new WaitUntil(() => PlayerScanner.IsTarget == true);

        while (Ammo > 0)
        {
            if (PlayerScanner.NearTarget == null) { break; }

            Vector3 targetPos = PlayerScanner.NearTarget.position;

            // ����ź �Ѿ�
            GameObject bullet = GameManager.Instance.PoolManager.GetBullet((int)BulletType.GrenadeBullet, this.transform.position);
            Vector3 velocitay = GetVelocity(this.transform.position, targetPos, 45f);

            bullet.GetComponent<PlayerAttack>().Init(Damage, Penetrate, velocitay, bulletVelocity);

            yield return new WaitForSeconds(Rate);
        }

        StartCoroutine(CoolDownCO(ReloadTime, coroutin));
    }
    #endregion // �ڷ�ƾ
}
