using UnityEngine;
using System;
using System.Collections.Generic;

public enum UIClipType
{
    Undefined,
    ButtonClick,
    OpenMenu,
    CloseMenu,
}

public enum SFXType
{
    Undefined,
    pasos,
    interaccionCorrecta,
    interaccionIncorrecta,
    Latido1,
    Latido2,
    Latido3,
    LatidoConMuerte,
}

public enum MusicType
{
    Undefined,
    MainMenu,
    MainGameplay,
    BackgroundMusic,
	Ambiente1,
	Ambiente2,
    SountrackCalmaSinLatidos,
    SountrackDarkCompleto,
}


[Serializable]
public struct UIClipData
{
    public UIClipType type;
    public AudioClip clip;
}

[Serializable]
public struct SFXClipData
{
    public SFXType type;
    public AudioClip clip;
}

[Serializable]
public struct MusicClipData
{
    public MusicType type;
    public AudioClip clip;
}

[CreateAssetMenu(
    fileName = "MusicDatabase",
    menuName = "Audio/Music Database"
)]
public class MusicDatabase : ScriptableObject
{
    [Header("UI Clips")]
    [SerializeField] private List<UIClipData> uiClips = new();

    [Header("SFX Clips")]
    [SerializeField] private List<SFXClipData> sfxClips = new();

    [Header("Music Clips")]
    [SerializeField] private List<MusicClipData> musicClips = new();

    public AudioClip GetUIClip(UIClipType type)
    {
        foreach (var data in uiClips)
            if (data.type == type)
                return data.clip;

        return null;
    }

    public AudioClip GetSFXClip(SFXType type)
    {
        foreach (var data in sfxClips)
            if (data.type == type)
                return data.clip;

        return null;
    }

    public AudioClip GetMusicClip(MusicType type)
    {
        foreach (var data in musicClips)
            if (data.type == type)
                return data.clip;

        return null;
    }
}

