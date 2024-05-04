using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;

#region ����
[System.Serializable]
public class Sound
{
    public string soundName;
    public AudioClip soundClip;
}
#endregion // ����

public class AudioManager : Singleton<AudioManager>
{
    #region ����
    [SerializeField] private AudioMixer audioMixer;

    [Header("=====> BGM Setting <=====")]
    [SerializeField] private Sound[] bgmSound;
    [SerializeField] private AudioSource bgmAudioSource;

    [Header("=====> SFX Setting <=====")]
    [SerializeField] private Sound[] sfxSound;
    [SerializeField] private AudioSource sfxAudioSource;
    #endregion // ����

    #region ������Ƽ
    #endregion // ������Ƽ

    #region �Լ�
    /** �ʱ�ȭ */
    public override void Awake()
    {
        base.Awake();

        AudioSourceInit();
    }

    /** ����� �ҽ� �⺻���� */
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

    /** ������� ����Ѵ� */
    public void PlayBGM(string soundName)
    {
        Sound sound = Array.Find(bgmSound, x => x.soundName == soundName);
        if(sound == null) { Debug.Log(" ������� �����ϴ� "); return; }

        // ������� ������� ���
        if (bgmAudioSource.isPlaying)
        {
            bgmAudioSource.Stop();
        }

        bgmAudioSource.clip = sound.soundClip;
        bgmAudioSource.Play();
    }

    /** ȿ������ ����Ѵ� */
    public void PlaySFX(string soundName)
    {
        Sound sound = Array.Find(bgmSound, x => x.soundName == soundName);
        if (sound == null) { Debug.Log(" ������� �����ϴ� "); return; }

        sfxAudioSource.PlayOneShot(sound.soundClip);
    }

    /** ������� ����� */
    public void StopBGM()
    {
        if(!bgmAudioSource.isPlaying) { return; }

        if (bgmAudioSource.isPlaying)
        {
            bgmAudioSource.Stop();
        }
    }
    #endregion // �Լ�
}
