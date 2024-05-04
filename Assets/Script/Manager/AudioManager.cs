using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;

#region 종류
[System.Serializable]
public class Sound
{
    public string soundName;
    public AudioClip soundClip;
}
#endregion // 종류

public class AudioManager : Singleton<AudioManager>
{
    #region 변수
    [SerializeField] private AudioMixer audioMixer;

    [Header("=====> BGM Setting <=====")]
    [SerializeField] private Sound[] bgmSound;
    [SerializeField] private AudioSource bgmAudioSource;

    [Header("=====> SFX Setting <=====")]
    [SerializeField] private Sound[] sfxSound;
    [SerializeField] private AudioSource sfxAudioSource;
    #endregion // 변수

    #region 프로퍼티
    #endregion // 프로퍼티

    #region 함수
    /** 초기화 */
    public override void Awake()
    {
        base.Awake();

        AudioSourceInit();
    }

    /** 오디오 소스 기본설정 */
    private void AudioSourceInit()
    {
        bgmAudioSource.outputAudioMixerGroup = audioMixer.FindMatchingGroups("BGM")[0];
        sfxAudioSource.outputAudioMixerGroup = audioMixer.FindMatchingGroups("SFX")[0];

        bgmAudioSource.playOnAwake = false;
        sfxAudioSource.playOnAwake = false;

        bgmAudioSource.loop = true;
        sfxAudioSource.loop = false;

        bgmAudioSource.volume = 0.1f;
        sfxAudioSource.volume = 0.1f;
    }

    /** 배경음을 재생한다 */
    public void PlayBGM(string soundName)
    {
        Sound sound = Array.Find(bgmSound, x => x.soundName == soundName);
        if(sound == null) { Debug.Log(" 배경음이 없습니다 "); return; }

        // 배경음이 재생중일 경우
        if (bgmAudioSource.isPlaying)
        {
            bgmAudioSource.Stop();
        }

        bgmAudioSource.clip = sound.soundClip;
        bgmAudioSource.Play();
    }

    /** 효과음을 재생한다 */
    public void PlaySFX(string soundName)
    {
        Sound sound = Array.Find(bgmSound, x => x.soundName == soundName);
        if (sound == null) { Debug.Log(" 배경음이 없습니다 "); return; }

        sfxAudioSource.PlayOneShot(sound.soundClip);
    }

    /** 배경음을 멈춘다 */
    public void StopBGM()
    {
        if(!bgmAudioSource.isPlaying) { return; }

        if (bgmAudioSource.isPlaying)
        {
            bgmAudioSource.Stop();
        }
    }
    #endregion // 함수
}
