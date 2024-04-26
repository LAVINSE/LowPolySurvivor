using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class MiniMapUI : MonoBehaviour
{
    #region ����
    [SerializeField] private TMP_Text mapNameText;
    [SerializeField] private float zoomMin = 1f;
    [SerializeField] private float zoomMax = 30f;
    [SerializeField] private float zoomStep = 1f;
    #endregion // ����

    #region ������Ƽ
    public Camera MiniMapCamera { get; set; }
    #endregion // ������Ƽ

    #region �Լ�
    /** �⺻ ���� */
    public void Init(string mapName)
    {
        mapNameText.text = mapName;
    }

    /** �̴ϸ� �� �� */
    public void ZoomIn()
    {
        MiniMapCamera.orthographicSize = Mathf.Max(MiniMapCamera.orthographicSize - zoomStep, zoomMin);
    }

    /** �̴ϸ� �� �ƿ� */
    public void ZoomOut()
    {
        MiniMapCamera.orthographicSize = Mathf.Min(MiniMapCamera.orthographicSize + zoomStep, zoomMax);
    }
    #endregion // �Լ�
}
