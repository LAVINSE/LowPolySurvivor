using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class InventoryItem : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    #region 변수
    [Header(" 기본 설정 ")]
    [SerializeField] private Image image; // 아이템 이미지
    [SerializeField] private TMP_Text countText; // 아이템 개수 텍스트
    #endregion // 변수

    #region 프로퍼티
    public int ItemCount { get; set; } = 1; // 아이템 개수

    public ItemSO ItemSO { get; set; } // 아이템 데이터
    public Transform ParentAfterDrag { get; set; } // 이전 슬롯으로 돌아가도록 설정할 변수
    #endregion // 프로퍼티

    #region 함수
    /** 인벤토리 아이템 기본설정 초기화 */
    public void InitItem(ItemSO itemSO)
    {
        this.ItemSO = itemSO;
        image.sprite = itemSO.itemImage;

        // 인벤토리 아이템 스텍 수 설정
        ItemStackCountSetting();
    }

    /** 인벤토리 아이템 스텍 수 설정 및 텍스트 */
    public void ItemStackCountSetting()
    {
        // 텍스트에 표시
        countText.text = ItemCount.ToString();

        // 아이템 수량이 2개부터 텍스트 활성화
        bool textAcitve = ItemCount > 1;
        countText.gameObject.SetActive(textAcitve);
    }

    /** 드래그를 시작한다 */
    public void OnBeginDrag(PointerEventData eventData)
    {
        image.raycastTarget = false;
        
        // 현재 슬롯 저장
        ParentAfterDrag = this.transform.parent;

        // 캔버스로 부모설정
        this.transform.SetParent(this.transform.root);
    }

    /** 드래그 중일때 */
    public void OnDrag(PointerEventData eventData)
    {
        // 아이템의 위치를 마우스 위치로
        this.transform.position = Input.mousePosition;
    }

    /** 드래그를 종료한다 */
    public void OnEndDrag(PointerEventData eventData)
    {
        image.raycastTarget = true;

        // 원래 슬롯으로 돌아감
        this.transform.SetParent(ParentAfterDrag);
    }
    #endregion // 함수
}
