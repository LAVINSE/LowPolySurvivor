using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    #region ����
    public InventorySlot[] inventorySlots;
    public GameObject inventoryItemPrefab;
    #endregion // ����

    #region �Լ�
    public void AddItem(Item item)
    {
        // Find any empty slot
        for(int i = 0; i < inventorySlots.Length; i++)
        {
            InventorySlot slot = inventorySlots[i];
            InventoryItem itemInslot = slot.GetComponentInChildren<InventoryItem>();

            if(itemInslot == null)
            {
                SpawnNewItem(item, slot);
                return;
            }
        }
    }

    private void SpawnNewItem(Item item, InventorySlot slot)
    {
        GameObject newItemGo = Instantiate(inventoryItemPrefab, slot.transform);
        InventoryItem inventoryItem = newItemGo.GetComponent<InventoryItem>();
        // Basic setting
        inventoryItem.InitialiseItem(item);
    }
    #endregion // �Լ�
}
