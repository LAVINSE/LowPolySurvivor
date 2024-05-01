using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SelectEventButtonUI : MonoBehaviour
{
    #region ����
    [SerializeField] private Button button;
    [SerializeField] private Image image;
    [SerializeField] private TMP_Text nameText;
    [SerializeField] private TMP_Text descText;
    #endregion // ����

    #region ������Ƽ
    public Button Button
    {
        get => button;
        set => button = value;
    }
    public Image Image
    {
        get => image;
        set => image = value;
    }
    public TMP_Text NameText
    {
        get => nameText;
        set => nameText = value;
    }
    public TMP_Text DescText
    {
        get => descText;
        set => descText = value;
    }

    public EventDataSO EventDataSO { get; set; }
    #endregion // ������Ƽ

    #region �Լ�
    public void Init()
    {
        Image.sprite = EventDataSO.eventImg;
        NameText.text = EventDataSO.eventName;
        DescText.text = EventDataSO.eventDesc;
    }
    #endregion // �Լ�
}
