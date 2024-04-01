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
    #endregion // 변수

    #region 프로퍼티
    public Weapon[] WeaponArray { get; set; }
    public float CurrentHp { get; set; }
    #endregion // 프로퍼티

    #region 함수
    /** 초기화 */
    private void Awake()
    {
        // 현재 체력 설정
        CurrentHp = maxHp;

        test();

        // TODO : 테스트
        if (weaponDict.ContainsKey(eEquipType.SubmachineGun))
        {
            GameObject asdf = weaponDict[eEquipType.SubmachineGun];
            //asdf.SetActive(true);
        }
    }

    private void test()
    {
        foreach(Transform weapon in weaponObject.transform)
        {
            var playerweapon = weapon.GetComponent<Weapon>();
            playerweapon.Init();
            weaponDict.Add(playerweapon.EquipType, weapon.gameObject);
        }
    }

    public void AddWeapon(eEquipType type)
    {
        
    }
    #endregion // 함수
}
