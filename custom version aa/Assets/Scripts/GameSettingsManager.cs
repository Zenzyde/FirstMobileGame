using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameSettingsManager : MonoBehaviour
{

    [SerializeField] private Settings settings;
    public Settings Settings
    {
        get
        {
            return settings;
        }
    }
    [SerializeField] private FixedSettingsCreator fixedSettings;
    public FixedSettingsCreator FixedSettings
    {
        get
        {
            return fixedSettings;
        }
    }

    private static GameSettingsManager instance;
    public static GameSettingsManager Instance
    {
        get
        {
            return instance;
        }
    }

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(this);
        }
        else if (instance != this)
        {
            Destroy(this);
        }
        StartCoroutine(LoadSettings()); //Did this because it would load the settings before the audiomanager instance was available
    }

    IEnumerator LoadSettings()
    {
        //SaveSettingsInstance();
        //LoadSettingsInstance();
        Screen.fullScreen = settings.FullScreen;
        //Screen.SetResolution(fixedSettings.ScreenResolutions[settings.ScreenRes].x,
        //fixedSettings.ScreenResolutions[settings.ScreenRes].y, settings.FullScreen);
        QualitySettings.SetQualityLevel(settings.GraphicsLevel);
        yield return new WaitUntil(() => (AudioManager.Instance != null));
        AudioManager.Instance.SetMusicVolume(settings.MusicVolume);
        AudioManager.Instance.SetSFXVolume(settings.SFXVolume);
    }

    public void SaveSettings()
    {
        SaveSettingsInstance();
    }

    public void SetFullScreen(UnityEngine.UI.Toggle fullscreen)
    {
        settings.FullScreen = fullscreen.isOn;
        Screen.fullScreen = fullscreen.isOn;
    }

    public void SetScreenResolution(TMPro.TMP_Dropdown res)
    {
        //Vector2Int resolution = fixedSettings.ScreenResolutions[res.value];
        //Screen.SetResolution(resolution.x, resolution.y, settings.FullScreen);
    }

    public void SetGraphicsLevel(TMPro.TMP_Dropdown quality)
    {
        settings.GraphicsLevel = quality.value;
        if (MenuManager.Instance.GetCurrentMenu() == MenuType.settings)
        {
            QualitySettings.SetQualityLevel(quality.value);
        }
        else
        {
            QualitySettings.SetQualityLevel(quality.value, false);
        }
    }

    public void SetMusicVolume(UnityEngine.UI.Slider volume)
    {
        settings.MusicVolume = volume.value;
    }

    public void SetSFXVolume(UnityEngine.UI.Slider volume)
    {
        settings.SFXVolume = volume.value;
    }

    public void SetMouseSens(UnityEngine.UI.Slider sens)
    {
        settings.MouseSens = sens.value;
        //CameraManager.Instance.SetMouseSens(settings.MouseSens);
    }

    public void SaveSettingsInstance()
    {
        DataSerializer.SaveSettings(settings);
    }

    public void LoadSettingsInstance()
    {
        settings = DataSerializer.GetSettings();
    }
}