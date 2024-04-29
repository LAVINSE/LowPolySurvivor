using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ItemDropSO", menuName = "Scriptable Objects/ItemDropSO/ItemDrop")]
public class ItemDropSO : ScriptableObject
{
    public PoolItemType itemType;
    public GameObject itemPrefab = null;
    public int dropChance = -1;
}
