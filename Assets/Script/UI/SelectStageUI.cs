using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class SelectStageUI : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    #region ����
    private RectTransform rect;
    #endregion // ����

    #region �Լ�
    /** �ʱ�ȭ */
    private void Awake()
    {
        rect = GetComponent<RectTransform>();
    }

    /** ���콺�� �������� */
    public void OnPointerEnter(PointerEventData eventData)
    {
        rect.localScale = Vector3.one * 1.1f;
    }

    /** ���콺�� �������� */
    public void OnPointerExit(PointerEventData eventData)
    {
        rect.localScale = Vector3.one;
    }
    #endregion // �Լ�
}
