using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class MiniMapUI : MonoBehaviour
{
    #region 변수
    [SerializeField] private TMP_Text mapNameText;
    [SerializeField] private float zoomMin = 1f;
    [SerializeField] private float zoomMax = 30f;
    [SerializeField] private float zoomStep = 1f;
    #endregion // 변수

    #region 프로퍼티
    public Camera MiniMapCamera { get; set; }
    #endregion // 프로퍼티

    #region 함수
    /** 기본 설정 */
    public void Init(string mapName)
    {
        mapNameText.text = mapName;
    }

    /** 미니맵 줌 인 */
    public void ZoomIn()
    {
        MiniMapCamera.orthographicSize = Mathf.Max(MiniMapCamera.orthographicSize - zoomStep, zoomMin);
    }

    /** 미니맵 줌 아웃 */
    public void ZoomOut()
    {
        MiniMapCamera.orthographicSize = Mathf.Min(MiniMapCamera.orthographicSize + zoomStep, zoomMax);
    }
    #endregion // 함수
}
