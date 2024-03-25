using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ItemDataSO", menuName = "Scriptable Objects/ItemDataSO/ItemData")]
public class ItemDataSO : ScriptableObject
{
    public enum EItemType
    {
        None,
        Exp,
        Upgrade,
    }

    public EItemType itemtype = EItemType.None;
    public string itemName = string.Empty;
    public GameObject itemPrefab = null;
    public int dropChance = -1;
}
