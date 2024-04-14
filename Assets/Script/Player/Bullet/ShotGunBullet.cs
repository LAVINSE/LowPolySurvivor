using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShotGunBullet : PlayerAttack
{
    #region 변수
    [SerializeField] private float knockBackpower = 1.3f;
    #endregion // 변수

    #region 함수
    public override void Attack(Enemy enemy)
    {
        base.Attack(enemy);

        StartCoroutine(KnockBackCO(enemy));

        rigid.velocity = Vector3.zero;
        this.gameObject.SetActive(false);
    }

    public override void Ground()
    {
        base.Ground();

        rigid.velocity = Vector3.zero;
        this.gameObject.SetActive(false);
    }

    /** 넉백 */
    private IEnumerator KnockBackCO(Enemy enemy)
    {
        Vector3 direction = enemy.transform.position - this.transform.position;
        direction.y = 0;

        enemy.Rigid.AddForce(direction.normalized * knockBackpower, ForceMode.Impulse);

        yield return null;

        enemy.Rigid.velocity = Vector3.zero;
    }
    #endregion // 함수
}
