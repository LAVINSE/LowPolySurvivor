using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum eEquipType
{
    None,
    SubmachineGun,

}

[CreateAssetMenu(fileName = "EquipWeaponDataSO", menuName = "Scriptable Objects/EquipWeaponDataSO/EquipWeaponData")]
public class EquipWeaponDataSO : ScriptableObject
{
    public eEquipType equipType = eEquipType.None;

    [Space]
    public string weaponName = string.Empty;
    [TextArea] 
    public string weaponDesc = string.Empty;

    [Space]
    public int maxLevel = 0; // 무기 최대 레벨
    public int baseLevel = 0; // 레벨
    public int baseAmmo = 0; // 탄창
    public int baseCount = 0; // 개수
    public float baseReloadTime = 0f; // 재장전 시간
    public float baseRange = 0f; // 사거리
    public float baseDamage = 0f; // 데미지
    public float baseRate = 0f; // 연사속도

    [Space]
    public GameObject prefab;
}
