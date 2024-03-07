using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class InventorySlot : MonoBehaviour, IDropHandler
{
    #region 변수
    [Header(" 기본 설정 ")]
    [SerializeField] private Image image; // 슬롯 이미지
    [SerializeField] private Color selectedColor; // 선택된 슬롯 색상
    [SerializeField] private Color notSelectedColor; // 선택되지 않은 슬롯 색상
    #endregion // 변수

    #region 함수
    /** 초기화 */
    private void Awake()
    {
        // 기본값으로 설정
        // 선택되지 않은 아이템 슬롯
        Deselcet();
    }

    /** 선택된 아이템 슬롯 */
    public void Select()
    {
        // 색상 변경
        image.color = selectedColor;
    }

    /** 선택되지 않은 아이템 슬롯 */
    public void Deselcet()
    {
        // 색상 변경
        image.color = notSelectedColor;
    }

    /** 아이템 드롭시 호출한다 */
    public void OnDrop(PointerEventData eventData)
    {
        // 해당 슬롯 자식에 아이템이 없을 경우
        if(this.transform.childCount == 0)
        {
            // 드래그된 객체에서 InventoryItem 컴포넌트를 가져온다
            InventoryItem inventoryItem = eventData.pointerDrag.GetComponent<InventoryItem>();
            if(inventoryItem != null)
            {
                // 현재 슬롯으로 부모 변경
                inventoryItem.ParentAfterDrag = this.transform;
            }  
        }
    }
    #endregion // 함수
}