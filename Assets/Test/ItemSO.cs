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

    [Header(" 아이템 타입 ")]
    public eItemType type = eItemType.None;

    [Header(" 아이템 설정 ")]
    [Tooltip(" 스택 가능한지 확인 ")] public bool isStack = true;
    [Tooltip(" 아이템 이미지 ")] public Sprite itemImage;
}
