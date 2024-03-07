using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    #region ����
    [SerializeField] private int maxStackedItems = 4; // ������ ���� �ִ� ����
    [SerializeField] private InventorySlot[] inventorySlots; // �κ��丮 ���� �迭
    [SerializeField] private GameObject inventoryItemPrefab; // �κ��丮 ������ ������

    private int selectedSlot = -1; // ���õ� ���� ��ȣ (-1�� �ʱⰪ)
    private int maxNumber = 7; // ���õ� ���� ��ȣ �ִ� ��
    #endregion // ����

    #region ������Ƽ
    #endregion // ������Ƽ

    #region �Լ�
    /** �ʱ�ȭ => ���¸� �����Ѵ� */
    private void Update()
    {
        // �Էµ� ���� ���� ���
        if(Input.inputString != null)
        {
            // �Էµ� ���ڿ��� ������ ������ ��ȯ�� �õ��ϰ� �����ϸ� true, �����ϸ� false
            bool isNumber = int.TryParse(Input.inputString, out int number);

            //  ��ȯ�� ���� ���� ���, 0���� ũ��, �ִ밪 �������� ��� 
            if(isNumber && number > 0 && number <= maxNumber)
            {
                // ������ �����ϰ�, ���õ� ������ number - 1�� �����Ѵ�
                ChangeSelectedSlot(number - 1);
            }
        }
    }

    /** ������ �����ϰ�, ���õ� ������ �����Ѵ� */
    private void ChangeSelectedSlot(int slotNumber)
    {
        // ���õ� ������ ���� ���
        if(selectedSlot >= 0)
        {
            // ���� ���õ� ���԰��� �������
            inventorySlots[selectedSlot].Deselcet();
        }

        // slotNumber������ ���õ� �������� �����Ѵ�
        inventorySlots[slotNumber].Select();
        // ���õ� ���� ��ȣ�� �����Ѵ�
        selectedSlot = slotNumber;
    }

    /** �������� �߰��Ѵ� */
    public bool AddItem(ItemSO item)
    {
        // ��� ���� �ݺ�
        for (int i = 0; i < inventorySlots.Length; i++)
        {
            InventorySlot slot = inventorySlots[i];
            InventoryItem inventoryItem = slot.GetComponentInChildren<InventoryItem>();

            // �κ��丮�� �������� �����ϰ�, ������ �����Ͱ� �߰��ϴ� �����۰� ����, �������� ������ �ִ� ������ ���� �ʰ�, ��ĥ �� ���� ���
            if (inventoryItem != null && inventoryItem.ItemSO == item && inventoryItem.ItemCount < maxStackedItems
                && inventoryItem.ItemSO.stackable == true)
            {
                // ������ ���� ����
                inventoryItem.ItemCount++;

                // �κ��丮 ������ ���� �� ���� �� �ؽ�Ʈ
                inventoryItem.ItemStackCountSetting();

                // ����
                return true;
            }
        }

        // ��� ���� �ݺ�
        for (int i = 0; i < inventorySlots.Length; i++)
        {
            InventorySlot slot = inventorySlots[i];
            InventoryItem inventoryItem = slot.GetComponentInChildren<InventoryItem>();

            // �κ��丮�� �������� ���� ���
            if(inventoryItem == null)
            {
                // �κ��丮�� ������ ����
                SpawnInventoryNewItem(item, slot);

                // ����
                return true;
            }
        }

        // ����
        return false;
    }

    /** �κ��丮�� �������� �����Ѵ� */
    private void SpawnInventoryNewItem(ItemSO item, InventorySlot slot)
    {
        // ���� �ڽĿ� ����
        GameObject inventoryNewItem = Instantiate(inventoryItemPrefab, slot.transform);
        InventoryItem inventoryItem = inventoryNewItem.GetComponent<InventoryItem>();

        // �κ��丮 ������ �⺻����
        inventoryItem.InitItem(item);
    }

    /** ���õ� �������� �����´� */
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
