using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class RockGunBullet : PlayerAttack
{
    #region ����
    [SerializeField] private float angularPower = 2f;
    [SerializeField] private float scaleValue = 0.1f;
    [SerializeField] private float angularPowerUp = 0.04f;
    [SerializeField] private float scaleValueUp = 0.01f;

    [SerializeField] private WaitForSeconds rockTime = new WaitForSeconds(0.1f);
    #endregion // ����

    #region �Լ�
    /** �ʱ�ȭ */
    protected override void OnEnable()
    {
        base.OnEnable();
        this.transform.localScale = Vector3.one;
        StartCoroutine(RockCO());
    }

    /** �ʱ�ȭ */
    protected override void OnDisable()
    {
        base.OnDisable();

        angularPower = 2f;
        scaleValue = 0.1f;
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
        yield return new WaitUntil(() => playerMain != null);

        while (true)
        {
            angularPower += angularPowerUp;
            scaleValue += scaleValueUp;
            this.transform.localScale = Vector3.one * scaleValue;
            rigid.AddTorque(this.transform.right * angularPower, ForceMode.Acceleration);

            if(attackRange < Vector3.Distance(this.transform.position, playerMain.transform.position))
            {
                Ground();
            }

            yield return rockTime;
        }
    }
    #endregion // �ڷ�ƾ
}
