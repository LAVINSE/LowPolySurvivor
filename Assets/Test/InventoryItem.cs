using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class InventoryItem : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    #region ����
    [Header(" �⺻ ���� ")]
    [SerializeField] private Image image; // ������ �̹���
    [SerializeField] private TMP_Text countText; // ������ ���� �ؽ�Ʈ
    #endregion // ����

    #region ������Ƽ
    public int ItemCount { get; set; } = 1; // ������ ����

    public ItemSO ItemSO { get; set; } // ������ ������
    public Transform ParentAfterDrag { get; set; } // ���� �������� ���ư����� ������ ����
    #endregion // ������Ƽ

    #region �Լ�
    /** �κ��丮 ������ �⺻���� �ʱ�ȭ */
    public void InitItem(ItemSO itemSO)
    {
        this.ItemSO = itemSO;
        image.sprite = itemSO.itemImage;

        // �κ��丮 ������ ���� �� ����
        ItemStackCountSetting();
    }

    /** �κ��丮 ������ ���� �� ���� �� �ؽ�Ʈ */
    public void ItemStackCountSetting()
    {
        // �ؽ�Ʈ�� ǥ��
        countText.text = ItemCount.ToString();

        // ������ ������ 2������ �ؽ�Ʈ Ȱ��ȭ
        bool textAcitve = ItemCount > 1;
        countText.gameObject.SetActive(textAcitve);
    }

    /** �巡�׸� �����Ѵ� */
    public void OnBeginDrag(PointerEventData eventData)
    {
        image.raycastTarget = false;
        
        // ���� ���� ����
        ParentAfterDrag = this.transform.parent;

        // ĵ������ �θ���
        this.transform.SetParent(this.transform.root);
    }

    /** �巡�� ���϶� */
    public void OnDrag(PointerEventData eventData)
    {
        // �������� ��ġ�� ���콺 ��ġ��
        this.transform.position = Input.mousePosition;
    }

    /** �巡�׸� �����Ѵ� */
    public void OnEndDrag(PointerEventData eventData)
    {
        image.raycastTarget = true;

        // ���� �������� ���ư�
        this.transform.SetParent(ParentAfterDrag);
    }
    #endregion // �Լ�
}
