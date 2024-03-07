using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    #region 변수
    [SerializeField] private int maxStackedItems = 4; // 아이템 스택 최대 개수
    [SerializeField] private InventorySlot[] inventorySlots; // 인벤토리 슬롯 배열
    [SerializeField] private GameObject inventoryItemPrefab; // 인벤토리 아이템 프리팹

    private int selectedSlot = -1; // 선택된 슬롯 번호 (-1은 초기값)
    private int maxNumber = 7; // 선택된 슬롯 번호 최대 값
    #endregion // 변수

    #region 프로퍼티
    #endregion // 프로퍼티

    #region 함수
    /** 초기화 => 상태를 갱신한다 */
    private void Update()
    {
        // 입력된 값이 있을 경우
        if(Input.inputString != null)
        {
            // 입력된 문자열을 가져와 정수로 변환을 시도하고 성공하면 true, 실패하면 false
            bool isNumber = int.TryParse(Input.inputString, out int number);

            //  변환에 성공 했을 경우, 0보다 크고, 최대값 보다작을 경우 
            if(isNumber && number > 0 && number <= maxNumber)
            {
                // 슬롯을 선택하고, 선택된 슬롯을 number - 1로 변경한다
                ChangeSelectedSlot(number - 1);
            }
        }
    }

    /** 슬롯을 선택하고, 선택된 슬롯을 변경한다 */
    private void ChangeSelectedSlot(int slotNumber)
    {
        // 선택된 슬롯이 있을 경우
        if(selectedSlot >= 0)
        {
            // 전에 선택된 슬롯강조 원래대로
            inventorySlots[selectedSlot].Deselcet();
        }

        // slotNumber슬롯을 선택된 슬롯으로 설정한다
        inventorySlots[slotNumber].Select();
        // 선택된 슬롯 번호를 저장한다
        selectedSlot = slotNumber;
    }

    /** 아이템을 추가한다 */
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
