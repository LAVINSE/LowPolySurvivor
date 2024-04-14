using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum eEquipType
{
    None,
    SubmachineGun,
    AssaultGun,
    ShotGun,
    MachineGun,
    GrenadeGun,
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
    [Space]
    public int baseMaxAmmo = 0; // 최대 탄창
    [Space]
    public int baseWeaponCount = 0; // 개수
    [Space]
    public int basePenetrate = 0; // 관통 횟수, -1 무한 관통
    [Space]
    public float baseReloadTime = 0f; // 재장전 시간
    [Space]
    public float baseRange = 0f; // 사거리
    [Space]
    public float baseDamage = 0f; // 데미지
    [Space]
    public float baseRate = 0f; // 연사속도
    [Space]
    public float baseBulletVelocity = 0f; // 탄속

    [Space]
    public GameObject prefab;
}
