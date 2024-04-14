using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrenadeGunBullet : PlayerAttack
{
    #region ����
    [SerializeField] private BoxCollider boxCollider; // ����ź �ݶ��̴�
    #endregion // ����

    #region �Լ�
    public override void Attack(Enemy enemy)
    {
        base.Attack(enemy);

        rigid.velocity = Vector3.zero;
        this.gameObject.SetActive(false);
    }

    public override void Ground()
    {
        base.Ground();

        rigid.velocity = Vector3.zero;
        this.gameObject.SetActive(false);
    }
    #endregion // �Լ�

    #region �ڷ�ƾ
    #endregion // �ڷ�ƾ
}
