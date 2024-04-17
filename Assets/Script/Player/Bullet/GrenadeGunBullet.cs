using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrenadeGunBullet : PlayerAttack
{
    #region ����
    [SerializeField] private SphereCollider sphereCollider; // ����ź �ݶ��̴�
    #endregion // ����

    #region �Լ�
    /** �ʱ�ȭ */
    protected override void Awake()
    {
        base.Awake();

        sphereCollider.enabled = false;
    }

    /** �ʱ�ȭ */
    protected override void Start()
    {
        base.Start();

        StartCoroutine(ExplosionCO());
    }

    public override void Attack(Enemy enemy)
    {
        base.Attack(enemy);
    }

    public override void Ground()
    {
        base.Ground();

        rigid.velocity = Vector3.zero;
        this.gameObject.SetActive(false);
    }
    #endregion // �Լ�

    #region �ڷ�ƾ
    /** ����ź ���� */
    private IEnumerator ExplosionCO()
    {
        // 3�� ���
        yield return new WaitForSeconds(10f);

        sphereCollider.GetComponent<PlayerWeaponAttack>().Init(AttackDamage);

        rigid.velocity = Vector3.zero;
        rigid.angularVelocity = Vector3.zero;

        sphereCollider.enabled = true;

        // 1�� �ܻ�
        yield return new WaitForSeconds(1f);

        sphereCollider.enabled = false;

        this.gameObject.SetActive(false);
    }
    #endregion // �ڷ�ƾ
}
