using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ItemSO",menuName = "Scriptable Objects/ItemSo/Item")]
public class ItemSO : ScriptableObject
{
    public enum eItemType
    {
        None,
    }

    [Header(" ������ Ÿ�� ")]
    public eItemType type = eItemType.None;

    [Header(" ������ ���� ")]
    [Tooltip(" ���� �������� Ȯ�� ")] public bool isStack = true;
    [Tooltip(" ������ �̹��� ")] public Sprite itemImage;
}
