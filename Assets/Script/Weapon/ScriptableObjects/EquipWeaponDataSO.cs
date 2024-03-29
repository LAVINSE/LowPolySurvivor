using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "EquipWeaponDataSO", menuName = "Scriptable Objects/EquipWeaponDataSO/EquipWeaponData")]
public class EquipWeaponDataSO : ScriptableObject
{
    public enum eEquipType
    {
        None,
        Projectile,

    }

    public eEquipType equipType = eEquipType.None;

    [Space]
    public string weaponName = string.Empty;
    [TextArea] 
    public string weaponDesc = string.Empty;

    [Space]
    public int baseLevel = 0;
    public float baseDamage = 0f;
    public int baseCount = 0;
    public float baseReloadTime = 0f;

    [Space]
    public GameObject prefab;
}
