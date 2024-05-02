using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class OptionPopupUI : PopupUI
{
    #region 변수
    [SerializeField] private TMP_Dropdown graphicsDropDown;

    [SerializeField] private Slider masterVolumeSlider;
    [SerializeField] private Slider musicVolumeSlider;
    [SerializeField] private Slider sfxVolumeSlider;
    [SerializeField] private AudioMixer mainAudioMixer;

    [SerializeField] private GameObject popupUIObject;
    #endregion // 변수

    #region 함수
    /** 초기화 */
    private void Awake()
    {
        Option_Background_Img = popupUIObject;

        // 현재 음량 슬라이더에 설정
        mainAudioMixer.GetFloat("MasterVolume", out float masterVolume);
        masterVolumeSlider.value = masterVolume;

        mainAudioMixer.GetFloat("MusicVolume", out float musicVolume);
        musicVolumeSlider.value = musicVolume;

        mainAudioMixer.GetFloat("SFXVolume", out float sfxVolume);
        sfxVolumeSlider.value = sfxVolume;

        graphicsDropDown.value = QualitySettings.GetQualityLevel();
    }

    /** 그래픽 설정을 변경한다 */
    public void ChangeGraphicsQuality()
    {
        QualitySettings.SetQualityLevel(graphicsDropDown.value);
    }

    /** 전체 사운드를 변경한다 */
    public void ChangeMasterVolume()
    {
        mainAudioMixer.SetFloat("MasterVolume", masterVolumeSlider.value);
    }

    /** 음악 사운드를 변경한다 */
    public void ChangeMusicVolume()
    {
        mainAudioMixer.SetFloat("MusicVolume", musicVolumeSlider.value);
    }

    /** 효과음 사운드를 변경한다 */
    public void ChangeSFXVolume()
    {
        mainAudioMixer.SetFloat("SFXVolume", sfxVolumeSlider.value);
    }

    /** 옵션 팝업을 생성한다 */
    public static OptionPopupUI CreateOptionPopup(GameObject ParentObject)
    {
        var CreateOptionUI = Factory.CreateCloneObj<OptionPopupUI>("OptionPopup", Resources.Load<GameObject>("Prefabs/UI/OptionUI"),
            ParentObject,Vector3.zero, Vector3.one, Vector3.zero);
        return CreateOptionUI;
    }
    #endregion // 함수
}
