using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Settings
{
    //Liquid / Generation Direction Settings
    [SerializeField] private bool endless;
    public bool Endless
    {
        get
        {
            return endless;
        }
        set
        {
            endless = value;
        }
    }

    //Generation Direction Data Holders
    [HideInInspector] [SerializeField] private Vector3 initialPosition;
    public Vector3 InitialPosition
    {
        get
        {
            return initialPosition;
        }
        set
        {
            initialPosition = value;
        }
    }
    [HideInInspector] [SerializeField] private Vector3 startingPosition;
    public Vector3 StartingPosition
    {
        get
        {
            return startingPosition;
        }
        set
        {
            startingPosition = value;
        }
    }

    //General Settings Data Holders
    [HideInInspector] [SerializeField] private float musicVolume;
    public float MusicVolume
    {
        get
        {
            return musicVolume;
        }
        set
        {
            musicVolume = value;
        }
    }
    [HideInInspector] [SerializeField] private bool musicMuted;
    public bool MusicMuted
    {
        get
        {
            return musicMuted;
        }
        set
        {
            musicMuted = value;
        }
    }
    [HideInInspector] [SerializeField] private float sfxVolume;
    public float SFXVolume
    {
        get
        {
            return sfxVolume;
        }
        set
        {
            sfxVolume = value;
        }
    }
    [HideInInspector] [SerializeField] private bool sfxMuted;
    public bool SFXMuted
    {
        get
        {
            return sfxMuted;
        }
        set
        {
            sfxMuted = value;
        }
    }
    [HideInInspector] [SerializeField] private bool fullScreen;
    public bool FullScreen
    {
        get
        {
            return fullScreen;
        }
        set
        {
            fullScreen = value;
        }
    }
    [HideInInspector] [SerializeField] private int graphicsLevel;
    public int GraphicsLevel
    {
        get
        {
            return graphicsLevel;
        }
        set
        {
            graphicsLevel = value;
        }
    }
    [HideInInspector] [SerializeField] private int screenRes;
    public int ScreenRes
    {
        get
        {
            return screenRes;
        }
        set
        {
            screenRes = value;
        }
    }

    // [SerializeField] private Score[] scores;
    // public Score[] Scores
    // {
    // 	get
    // 	{
    // 		return scores;
    // 	}
    // }

    //Player settings & extra effect-gameobjects dataholders
    [SerializeField] private float mouseSens;
    public float MouseSens
    {
        get
        {
            return mouseSens;
        }
        set
        {
            mouseSens = value;
        }
    }
}