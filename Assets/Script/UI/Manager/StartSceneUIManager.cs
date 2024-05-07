using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StartSceneUIManager : UIManager
{
    #region 변수
    [SerializeField] private Button selectStageButton;
    [SerializeField] private Button optionButton;
    [SerializeField] private Button leaveButton;

    [SerializeField] private Button stageButton;
    [SerializeField] private Button stageButton_1;
    [SerializeField] private Button stageButton_2;

    [SerializeField] private Button backButton;
    [SerializeField] private Button selectLeaveButton;
    #endregion // 변수

    #region 함수
    protected override void Awake()
    {
        base.Awake();
        selectStageButton.onClick.AddListener(() => AudioManager.Inst.PlaySFX("ClickSFX"));
        optionButton.onClick.AddListener(() => AudioManager.Inst.PlaySFX("ClickSFX"));
        leaveButton.onClick.AddListener(() => AudioManager.Inst.PlaySFX("ClickSFX"));
        stageButton.onClick.AddListener(() => AudioManager.Inst.PlaySFX("ClickSFX"));
        stageButton_1.onClick.AddListener(() => AudioManager.Inst.PlaySFX("ClickSFX"));
        stageButton_2.onClick.AddListener(() => AudioManager.Inst.PlaySFX("ClickSFX"));
        backButton.onClick.AddListener(() => AudioManager.Inst.PlaySFX("ClickSFX"));
        selectLeaveButton.onClick.AddListener(() => AudioManager.Inst.PlaySFX("ClickSFX"));
    }
    #endregion // 함수
}
