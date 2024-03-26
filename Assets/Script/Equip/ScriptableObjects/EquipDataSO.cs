using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "EquipDataSO", menuName = "Scriptable Objects/EquipDataSO/EquipData")]
public class EquipDataSO : ScriptableObject
{
    public enum eEquipType
    {
        None,
    }

    public eEquipType equipType = eEquipType.None;

    public float baseDamage = 0f;
    public int baseCount = 0;
    public float[] damages;
    public int[] counts;

    public GameObject prefab;
}
