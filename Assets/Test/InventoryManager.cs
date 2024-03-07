using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    #region ����
    public int maxStackedItems = 4;
    public InventorySlot[] inventorySlots;
    public GameObject inventoryItemPrefab;

    private int selectedSlot = -1;
    #endregion // ����

    #region �Լ�
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
        // ���� ���õ� ���԰��� �������
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

                // �κ��丮 ������ ���� �� ����
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

        // �κ��丮 ������ �⺻����
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
                    // �κ��丮 ������ ���� �� ����
                    itemInslot.ItemStackCountSetting();
                }
            }
            return item;
        }

        return null;
    }
    #endregion // �Լ�
}
