using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SelectEventButtonUI : MonoBehaviour
{
    #region 변수
    [SerializeField] private Button button;
    [SerializeField] private Image image;
    [SerializeField] private TMP_Text nameText;
    [SerializeField] private TMP_Text descText;
    #endregion // 변수

    #region 프로퍼티
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
    #endregion // 프로퍼티
}
