using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum eEquipType
{
    None = 0,
    SubmachineGun = 1,
    AssaultGun = 2,
    ShotGun = 3,
    MachineGun = 4,
    GrenadeGun = 5,
    RockGun = 6,
    Max_Value,
}


[CreateAssetMenu(fileName = "EquipWeaponDataSO", menuName = "Scriptable Objects/EquipWeaponDataSO/EquipWeaponDataSO")]
public class EquipWeaponDataSO : ScriptableObject
{
    public eEquipType equipType = eEquipType.None;

    [Header("=====> 무기 이름, 설명 <=====")]
    [Space]
    [Tooltip(" 무기 이름 ")] public string weaponName = string.Empty;
    [TextArea]
    [Tooltip(" 무기 설명 ")] public string weaponDesc = string.Empty;

    [Header("=====> 무기 레벨 <=====")]
    [Tooltip(" 무기 최대 레벨 ")] public int maxLevel = 0; // 무기 최대 레벨
    [Tooltip(" 무기 시작 레벨 ")] public int baseLevel = 0; // 레벨

    [Header("=====> 무기 탄창 <=====")]
    [Tooltip(" 무기 최대 탄창 ")] public int baseMaxAmmo = 0; // 최대 탄창

    [Header("=====> 무기 개수 <=====")]
    [Tooltip(" 무기 개수 ")] public int MaxWeaponCount = 0; // 개수
    [Tooltip(" 무기 개수 ")] public int baseWeaponCount = 0; // 개수

    [Header("=====> 무기 관통 <=====")]
    [Tooltip(" 무기 관통 횟수 (-1 무한) ")] public int basePenetrate = 0; // 관통 횟수, -1 무한 관통

    [Header("=====> 무기 재장전 <=====")]
    [Tooltip(" 무기 재장전 시간 ")] public float baseReloadTime = 0f; // 재장전 시간

    [Header("=====> 무기 범위 <=====")]
    [Tooltip(" 무기 범위 (-1 적 탐지 범위) ")] public float baseRange = 0f; // 사거리

    [Header("=====> 무기 데미지 <=====")]
    [Tooltip(" 무기 데미지 ")] public float baseDamage = 0f; // 데미지

    [Header("=====> 무기 연사속도 <=====")]
    [Tooltip(" 무기 연사속도 ")] public float baseRate = 0f; // 연사속도

    [Header("=====> 무기 탄속 <=====")]
    [Tooltip(" 무기 탄속 ")] public float baseBulletVelocity = 0f; // 탄속

    [Header("=====> 무기 프리팹 <=====")]
    [Tooltip(" 무기 프리팹 ")] public GameObject prefab;

    [Header("=====> 무기 이미지 <=====")]
    [Tooltip(" 무기 프리팹 ")] public Sprite weaponSprite;
}
