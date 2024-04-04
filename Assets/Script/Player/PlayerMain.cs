using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMain : MonoBehaviour
{
    #region 변수
    [Header("=====> 장착 장비 <=====")]
    [SerializeField] private GameObject weaponObject;

    [Header("=====> 플레이어 정보 <=====")]
    [SerializeField] private float maxHp;
    [SerializeField] public int luck;

    private Dictionary<eEquipType, GameObject> weaponDict = new Dictionary<eEquipType, GameObject>();
    private Animator animator;
    #endregion // 변수

    #region 프로퍼티
    public List<Weapon> WeaponList { get; set; } = new List<Weapon>();
    public float CurrentHp;
    #endregion // 프로퍼티

    #region 함수
    /** 초기화 */
    private void Awake()
    {
        animator = GetComponent<Animator>();

        // 현재 체력 설정
        CurrentHp = maxHp;

        InitWeapon();
        //ActiveAddWeapon(eEquipType.SubmachineGun);
    }

    /** 데미지를 받는다 */
    public void TakeDamage(float damage)
    {
        animator.SetTrigger("hitTrigger");

        CurrentHp -= damage;

        if (CurrentHp <= 0)
        {
            // 죽음
            Die();
        }
    }

    /** 플레이어 죽음 */
    private void Die()
    {

    }

    /** 무기 데이터 설정 및 딕셔너리에 추가한다 */
    private void InitWeapon()
    {
        foreach(Transform weapon in weaponObject.transform)
        {
            var playerweapon = weapon.GetComponent<Weapon>();
            playerweapon.Init();
            weaponDict.Add(playerweapon.EquipType, weapon.gameObject);
        }
    }

    /** 활성화 할 무기를 정한다 */
    public void ActiveAddWeapon(eEquipType type)
    {
        if (weaponDict.ContainsKey(type))
        {
            GameObject weapon = weaponDict[type];
            weapon.SetActive(true);
            WeaponList.Add(weapon.GetComponent<Weapon>());
        }
    }
    #endregion // 함수
}
