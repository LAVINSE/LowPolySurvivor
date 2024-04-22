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

    [Header("=====> ���� �̸�, ���� <=====")]
    [Space]
    [Tooltip(" ���� �̸� ")] public string weaponName = string.Empty;
    [TextArea]
    [Tooltip(" ���� ���� ")] public string weaponDesc = string.Empty;

    [Header("=====> ���� ���� <=====")]
    [Tooltip(" ���� �ִ� ���� ")] public int maxLevel = 0; // ���� �ִ� ����
    [Tooltip(" ���� ���� ���� ")] public int baseLevel = 0; // ����

    [Header("=====> ���� źâ <=====")]
    [Tooltip(" ���� �ִ� źâ ")] public int baseMaxAmmo = 0; // �ִ� źâ

    [Header("=====> ���� ���� <=====")]
    [Tooltip(" ���� ���� ")] public int MaxWeaponCount = 0; // ����
    [Tooltip(" ���� ���� ")] public int baseWeaponCount = 0; // ����

    [Header("=====> ���� ���� <=====")]
    [Tooltip(" ���� ���� Ƚ�� (-1 ����) ")] public int basePenetrate = 0; // ���� Ƚ��, -1 ���� ����

    [Header("=====> ���� ������ <=====")]
    [Tooltip(" ���� ������ �ð� ")] public float baseReloadTime = 0f; // ������ �ð�

    [Header("=====> ���� ���� <=====")]
    [Tooltip(" ���� ���� (-1 �� Ž�� ����) ")] public float baseRange = 0f; // ��Ÿ�

    [Header("=====> ���� ������ <=====")]
    [Tooltip(" ���� ������ ")] public float baseDamage = 0f; // ������

    [Header("=====> ���� ����ӵ� <=====")]
    [Tooltip(" ���� ����ӵ� ")] public float baseRate = 0f; // ����ӵ�

    [Header("=====> ���� ź�� <=====")]
    [Tooltip(" ���� ź�� ")] public float baseBulletVelocity = 0f; // ź��

    [Header("=====> ���� ������ <=====")]
    [Tooltip(" ���� ������ ")] public GameObject prefab;

    [Header("=====> ���� �̹��� <=====")]
    [Tooltip(" ���� ������ ")] public Sprite weaponSprite;
}
