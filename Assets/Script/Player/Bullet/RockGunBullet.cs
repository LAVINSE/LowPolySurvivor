using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RockGunBullet : PlayerAttack
{
    #region ����
    [SerializeField] private float angularPower = 2f;
    [SerializeField] private float scaleValue = 0.1f;
    [SerializeField] private float angularPowerUp = 0.02f;
    [SerializeField] private float scaleValueUp = 0.005f;
    #endregion // ����

    #region �Լ�
    protected override void Start()
    {
        base.Start();

        StartCoroutine(RockCO());
    }

    public override void Attack(Enemy enemy)
    {
        base.Attack(enemy);
        enemy.TakeDamage(AttackDamage);
    }

    public override void Ground()
    {
        base.Ground();

        rigid.velocity = Vector3.zero;
        this.gameObject.SetActive(false);
    }
    #endregion // �Լ�

    #region �ڷ�ƾ
    private IEnumerator RockCO()
    {
        while (true)
        {
            angularPower += angularPowerUp;
            scaleValue += scaleValueUp;
            this.transform.localScale = Vector3.one * scaleValue;
            rigid.AddTorque(this.transform.right * angularPower, ForceMode.Acceleration);
            yield return null;
        }
    }
    #endregion // �ڷ�ƾ
}
