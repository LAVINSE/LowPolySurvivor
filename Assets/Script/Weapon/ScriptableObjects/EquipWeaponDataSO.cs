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
    public int maxLevel = 0; // ���� �ִ� ����
    public int baseLevel = 0; // ����
    [Space]
    public int baseMaxAmmo = 0; // �ִ� źâ
    [Space]
    public int baseWeaponCount = 0; // ����
    [Space]
    public int basePenetrate = 0; // ���� Ƚ��, -1 ���� ����
    [Space]
    public float baseReloadTime = 0f; // ������ �ð�
    [Space]
    public float baseRange = 0f; // ��Ÿ�
    [Space]
    public float baseDamage = 0f; // ������
    [Space]
    public float baseRate = 0f; // ����ӵ�
    [Space]
    public float baseBulletVelocity = 0f; // ź��

    [Space]
    public GameObject prefab;
}
