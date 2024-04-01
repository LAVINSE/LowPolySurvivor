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
    public int maxLevel = 0; // ���� �ִ� ����
    public int baseLevel = 0; // ����
    public int baseAmmo = 0; // źâ
    public int baseCount = 0; // ����
    public float baseReloadTime = 0f; // ������ �ð�
    public float baseRange = 0f; // ��Ÿ�
    public float baseDamage = 0f; // ������
    public float baseRate = 0f; // ����ӵ�

    [Space]
    public GameObject prefab;
}
