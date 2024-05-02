using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class SelectStageUI : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    #region 변수
    private RectTransform rect;
    #endregion // 변수

    #region 함수
    /** 초기화 */
    private void Awake()
    {
        rect = GetComponent<RectTransform>();
    }

    /** 마우스가 들어왔을때 */
    public void OnPointerEnter(PointerEventData eventData)
    {
        rect.localScale = Vector3.one * 1.1f;
    }

    /** 마우스가 나갔을때 */
    public void OnPointerExit(PointerEventData eventData)
    {
        rect.localScale = Vector3.one;
    }
    #endregion // 함수
}
