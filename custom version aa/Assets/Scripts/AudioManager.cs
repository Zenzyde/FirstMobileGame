using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;
using System.Linq;

[RequireComponent(typeof(AudioSource))]
public class AudioManager : MonoBehaviour
{
    [SerializeField] private AudioClip musicClip;
    private AudioSource source;
    private bool stopMusic;

    private static AudioManager instance;
    public static AudioManager Instance
    {
        get
        {
            return instance;
        }
    }

    void Start()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(this);
            source = GetComponent<AudioSource>();
            source.clip = musicClip;
        }
        else if (instance != this)
        {
            Destroy(this);
        }
    }

    IEnumerator PlayMusic()
    {
        source.Play();
        while (!stopMusic)
        {
            if (!source.isPlaying)
            {
                source.Play();
                yield return null;
            }
        }
    }

    IEnumerator FadeTo(AudioClip clip)
    {
        while (source.volume > 0)
        {
            source.volume -= .1f;
            yield return null;
        }
        StopMusic();
        if (clip != null)
        {
            SetMusicClip(clip);
            while (source.volume < GameSettingsManager.Instance.Settings.MusicVolume)
            {
                source.volume += .1f;
                yield return null;
            }
            StartMusic();
        }
    }

    public void SwitchMusic(bool fade, AudioClip clip)
    {
        if (fade)
        {
            StartCoroutine(FadeTo(clip));
        }
        else
        {
            StopMusic();
            if (clip != null)
            {
                SetMusicClip(clip);
                StartMusic();
            }
        }
    }

    public void StartMusic()
    {
        stopMusic = false;
        StartCoroutine(PlayMusic());
    }

    public void StopMusic()
    {
        stopMusic = true;
    }

    public void SetMusicVolume(float volume)
    {
        source.volume = volume;
        GameSettingsManager.Instance.Settings.MusicVolume = volume;
    }

    public void SetMuted(bool muted)
    {
        source.mute = muted;
        GameSettingsManager.Instance.Settings.MusicMuted = muted;
    }

    public bool GetIsMuted()
    {
        return source.mute && GameSettingsManager.Instance.Settings.MusicMuted;
    }

    public void SetMusicClip(AudioClip clip)
    {
        source.clip = clip;
    }

    public void SetSFXVolume(float volume)
    {
        foreach (var player in FindObjectsOfType<AudioOneshotPlayer>())
        {
            player.SetVolume(volume);
        }
        GameSettingsManager.Instance.Settings.SFXVolume = volume;
    }

    public void SetSFXMuted(bool muted)
    {
        foreach (var player in FindObjectsOfType<AudioOneshotPlayer>())
        {
            player.SetMuted(muted);
        }
        GameSettingsManager.Instance.Settings.SFXMuted = muted;
    }

    public bool GetIsSFXMuted()
    {
        return GameSettingsManager.Instance.Settings.SFXMuted;
    }
}