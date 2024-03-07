using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class InventorySlot : MonoBehaviour, IDropHandler
{
    #region ����
    [Header(" �⺻ ���� ")]
    [SerializeField] private Image image; // ���� �̹���
    [SerializeField] private Color selectedColor; // ���õ� ���� ����
    [SerializeField] private Color notSelectedColor; // ���õ��� ���� ���� ����
    #endregion // ����

    #region �Լ�
    /** �ʱ�ȭ */
    private void Awake()
    {
        // �⺻������ ����
        // ���õ��� ���� ������ ����
        Deselcet();
    }

    /** ���õ� ������ ���� */
    public void Select()
    {
        // ���� ����
        image.color = selectedColor;
    }

    /** ���õ��� ���� ������ ���� */
    public void Deselcet()
    {
        // ���� ����
        image.color = notSelectedColor;
    }

    /** ������ ��ӽ� ȣ���Ѵ� */
    public void OnDrop(PointerEventData eventData)
    {
        // �ش� ���� �ڽĿ� �������� ���� ���
        if(this.transform.childCount == 0)
        {
            // �巡�׵� ��ü���� InventoryItem ������Ʈ�� �����´�
            InventoryItem inventoryItem = eventData.pointerDrag.GetComponent<InventoryItem>();
            if(inventoryItem != null)
            {
                // ���� �������� �θ� ����
                inventoryItem.ParentAfterDrag = this.transform;
            }  
        }
    }
    #endregion // �Լ�
}