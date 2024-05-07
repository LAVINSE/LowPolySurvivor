using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class OptionPopupUI : PopupUI
{
    #region ����
    [Header("=====> ��Ӵٿ� <=====")]
    [SerializeField] private TMP_Dropdown graphicsDropDown;

    [Header("=====> �����̴� <=====")]
    [SerializeField] private Slider masterVolumeSlider;
    [SerializeField] private Slider musicVolumeSlider;
    [SerializeField] private Slider sfxVolumeSlider;

    [Header("=====> ����� �ͼ� <=====")]
    [SerializeField] private AudioMixer mainAudioMixer;

    [Header("=====> ������Ʈ <=====")]
    [SerializeField] private GameObject popupUIObject;

    [Header("=====> ��ư <=====")]
    [SerializeField] private Button mainMenuButton;
    [SerializeField] private Button cancelButton;
    #endregion // ����

    #region �Լ�
    /** �ʱ�ȭ */
    private void Awake()
    {
        Option_Background_Img = popupUIObject;

        // ���� ���� �����̴��� ����
        mainAudioMixer.GetFloat("MasterVolume", out float masterVolume);
        masterVolumeSlider.value = masterVolume;

        mainAudioMixer.GetFloat("BGMVolume", out float musicVolume);
        musicVolumeSlider.value = musicVolume;

        mainAudioMixer.GetFloat("SFXVolume", out float sfxVolume);
        sfxVolumeSlider.value = sfxVolume;

        graphicsDropDown.value = QualitySettings.GetQualityLevel();

        mainMenuButton.onClick.AddListener(() => AudioManager.Inst.PlaySFX("ClickSFX"));
        cancelButton.onClick.AddListener(() => AudioManager.Inst.PlaySFX("ClickSFX"));
    }

    /** ���θ޴��� �̵� */
    public void ChangeSceneStartScene()
    {
        if(SceneManager.GetActiveScene().name == "StartScene")
        {
            PopupClose();
            return;
        }

        SceneManager.LoadScene("StartScene");
    }

    /** �׷��� ������ �����Ѵ� */
    public void ChangeGraphicsQuality()
    {
        QualitySettings.SetQualityLevel(graphicsDropDown.value);
    }

    /** ��ü ���带 �����Ѵ� */
    public void ChangeMasterVolume()
    {
        mainAudioMixer.SetFloat("MasterVolume", masterVolumeSlider.value);
    }

    /** ���� ���带 �����Ѵ� */
    public void ChangeMusicVolume()
    {
        mainAudioMixer.SetFloat("BGMVolume", musicVolumeSlider.value);
    }

    /** ȿ���� ���带 �����Ѵ� */
    public void ChangeSFXVolume()
    {
        mainAudioMixer.SetFloat("SFXVolume", sfxVolumeSlider.value);
    }

    /** �ɼ� �˾��� �����Ѵ� */
    public static OptionPopupUI CreateOptionPopup(GameObject ParentObject)
    {
        var CreateOptionUI = Factory.CreateCloneObj<OptionPopupUI>("OptionPopup", Resources.Load<GameObject>("Prefabs/UI/OptionUI"),
            ParentObject,Vector3.zero, Vector3.one, Vector3.zero);
        return CreateOptionUI;
    }
    #endregion // �Լ�
}
