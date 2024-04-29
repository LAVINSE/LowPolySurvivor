using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    #region 변수
    protected int Penetrate;

    protected float attackRange;
    protected float AttackDamage;

    protected Rigidbody rigid;
    protected PlayerMain playerMain;
    #endregion // 변수

    #region 프로퍼티
    #endregion // 프로퍼티

    #region 함수
    /** 초기화 */
    protected virtual void Awake()
    {
        rigid = GetComponent<Rigidbody>();
    }

    /** 초기화 */
    protected virtual void Start()
    {

    }

    /** 초기화 */
    protected virtual void OnEnable()
    {

    }

    /** 초기화 */
    protected virtual void OnDisable()
    {
        StopAllCoroutines();
    }

    /** 접촉했을 경우 (트리거) */
    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            Enemy enemy = other.gameObject.GetComponent<Enemy>();

            if(enemy != null)
            {
                Attack(enemy);

                if (Penetrate == 0)
                {
                    rigid.velocity = Vector3.zero;
                    this.gameObject.SetActive(false);
                }

                if (Penetrate != -1)
                {
                    Penetrate--;
                }
            }
        }

        if (other.gameObject.CompareTag("Ground"))
        {
            Ground();
        }
    }

    /** 공격 */
    public virtual void Attack(Enemy enemy) { }

    /** Ground 태그에 충돌 했을 경우 */
    public virtual void Ground() { }

    /** 기본설정 */
    public void Init(float attackDamage, int Penetrate, Vector3 direction, float rate = 0,
        float attackRange = -1, PlayerMain playerMain = null)
    {
        this.AttackDamage = attackDamage;
        this.Penetrate = Penetrate;
        this.attackRange = attackRange;
        this.playerMain = playerMain;

        rigid.velocity = direction * rate;
    }
    #endregion // 함수
}
