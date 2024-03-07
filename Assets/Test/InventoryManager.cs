using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    #region 변수
    public int maxStackedItems = 4;
    public InventorySlot[] inventorySlots;
    public GameObject inventoryItemPrefab;

    private int selectedSlot = -1;
    #endregion // 변수

    #region 함수
    private void Start()
    {
        ChangeSelectedSlot(0);
    }

    private void Update()
    {
        if(Input.inputString != null)
        {
            bool isNumber = int.TryParse(Input.inputString, out int number);
            if(isNumber && number > 0 && number < 8)
            {
                ChangeSelectedSlot(number - 1);
            }
        }
    }

    private void ChangeSelectedSlot(int newValue)
    {
        // 전에 선택된 슬롯강조 원래대로
        if(selectedSlot >= 0)
        {
            inventorySlots[selectedSlot].Deselcet();
        }
        
        inventorySlots[newValue].Select();
        selectedSlot = newValue;
    }

    public bool AddItem(ItemSO item)
    {
        // Check if any slot has the same item with count lower than max
        for (int i = 0; i < inventorySlots.Length; i++)
        {
            InventorySlot slot = inventorySlots[i];
            InventoryItem itemInslot = slot.GetComponentInChildren<InventoryItem>();

            if (itemInslot != null && itemInslot.ItemSO == item && itemInslot.ItemCount < maxStackedItems
                && itemInslot.ItemSO.stackable == true)
            {
                itemInslot.ItemCount++;

                // 인벤토리 아이템 스텍 수 설정
                itemInslot.ItemStackCountSetting();
                return true;
            }
        }

        // Find any empty slot
        for (int i = 0; i < inventorySlots.Length; i++)
        {
            InventorySlot slot = inventorySlots[i];
            InventoryItem itemInslot = slot.GetComponentInChildren<InventoryItem>();

            if(itemInslot == null)
            {
                SpawnNewItem(item, slot);
                return true;
            }
        }

        return false;
    }

    private void SpawnNewItem(ItemSO item, InventorySlot slot)
    {
        GameObject newItemGo = Instantiate(inventoryItemPrefab, slot.transform);
        InventoryItem inventoryItem = newItemGo.GetComponent<InventoryItem>();

        // 인벤토리 아이템 기본설정
        inventoryItem.InitItem(item);
    }

    public ItemSO GetSelectedItem(bool use)
    {
        InventorySlot slot = inventorySlots[selectedSlot];
        InventoryItem itemInslot = slot.GetComponentInChildren<InventoryItem>();

        if(itemInslot != null)
        {
            ItemSO item = itemInslot.ItemSO;
            if(use == true)
            {
                itemInslot.ItemCount--;

                if(itemInslot.ItemCount <= 0)
                {
                    Destroy(itemInslot.gameObject);
                }
                else
                {
                    // 인벤토리 아이템 스텍 수 설정
                    itemInslot.ItemStackCountSetting();
                }
            }
            return item;
        }

        return null;
    }
    #endregion // 함수
}
