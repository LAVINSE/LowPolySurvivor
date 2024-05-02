using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class OptionPopupUI : PopupUI
{
    #region ����
    [SerializeField] private TMP_Dropdown graphicsDropDown;

    [SerializeField] private Slider masterVolumeSlider;
    [SerializeField] private Slider musicVolumeSlider;
    [SerializeField] private Slider sfxVolumeSlider;
    [SerializeField] private AudioMixer mainAudioMixer;

    [SerializeField] private GameObject popupUIObject;
    #endregion // ����

    #region �Լ�
    /** �ʱ�ȭ */
    private void Awake()
    {
        Option_Background_Img = popupUIObject;

        // ���� ���� �����̴��� ����
        mainAudioMixer.GetFloat("MasterVolume", out float masterVolume);
        masterVolumeSlider.value = masterVolume;

        mainAudioMixer.GetFloat("MusicVolume", out float musicVolume);
        musicVolumeSlider.value = musicVolume;

        mainAudioMixer.GetFloat("SFXVolume", out float sfxVolume);
        sfxVolumeSlider.value = sfxVolume;

        graphicsDropDown.value = QualitySettings.GetQualityLevel();
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
        mainAudioMixer.SetFloat("MusicVolume", musicVolumeSlider.value);
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
