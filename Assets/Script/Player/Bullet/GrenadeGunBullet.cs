using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrenadeGunBullet : PlayerAttack
{
    #region 변수
    [SerializeField] private SphereCollider sphereCollider; // 수류탄 콜라이더
    #endregion // 변수

    #region 함수
    /** 초기화 */
    protected override void Awake()
    {
        base.Awake();

        sphereCollider.enabled = false;
    }

    protected override void OnEnable()
    {
        base.OnEnable();

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

    private void Active()
    {
        rigid.velocity = Vector3.zero;
        this.gameObject.SetActive(false);
    }
    #endregion // 함수

    #region 코루틴
    /** 수류탄 폭발 */
    private IEnumerator ExplosionCO()
    {
        // 3초 대기
        yield return new WaitForSeconds(3f);
        Debug.Log(" 펑 ");
        sphereCollider.GetComponent<PlayerWeaponAttack>().Init(AttackDamage);

        rigid.velocity = Vector3.zero;
        rigid.angularVelocity = Vector3.zero;

        sphereCollider.enabled = true;

        // 1초 잔상
        yield return new WaitForSeconds(1f);

        sphereCollider.enabled = false;

        Active();
    }
    #endregion // 코루틴
}
