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
        // 모든 슬롯 반복
        for (int i = 0; i < inventorySlots.Length; i++)
        {
            InventorySlot slot = inventorySlots[i];
            InventoryItem inventoryItem = slot.GetComponentInChildren<InventoryItem>();

            // 인벤토리에 아이템이 존재하고, 아이템 데이터가 추가하는 아이템과 같고, 아이템의 개수가 최대 개수를 넘지 않고, 합칠 수 있을 경우
            if (inventoryItem != null && inventoryItem.ItemSO == item && inventoryItem.ItemCount < maxStackedItems
                && inventoryItem.ItemSO.stackable == true)
            {
                // 아이템 개수 증가
                inventoryItem.ItemCount++;

                // 인벤토리 아이템 스텍 수 설정 및 텍스트
                inventoryItem.ItemStackCountSetting();

                // 성공
                return true;
            }
        }

        // 모든 슬롯 반복
        for (int i = 0; i < inventorySlots.Length; i++)
        {
            InventorySlot slot = inventorySlots[i];
            InventoryItem inventoryItem = slot.GetComponentInChildren<InventoryItem>();

            // 인벤토리에 아이템이 없을 경우
            if(inventoryItem == null)
            {
                // 인벤토리에 아이템 생성
                SpawnInventoryNewItem(item, slot);

                // 성공
                return true;
            }
        }

        // 실패
        return false;
    }

    /** 인벤토리에 아이템을 생성한다 */
    private void SpawnInventoryNewItem(ItemSO item, InventorySlot slot)
    {
        // 슬롯 자식에 생성
        GameObject inventoryNewItem = Instantiate(inventoryItemPrefab, slot.transform);
        InventoryItem inventoryItem = inventoryNewItem.GetComponent<InventoryItem>();

        // 인벤토리 아이템 기본설정
        inventoryItem.InitItem(item);
    }

    /** 선택된 아이템을 가져온다 */
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
