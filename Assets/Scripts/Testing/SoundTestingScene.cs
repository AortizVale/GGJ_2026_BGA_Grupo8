using UnityEngine;

public class SoundTestingScene : MonoBehaviour
{
    [Header("UI")]
    public UIClipType uiClip;

    [Header("SFX")]
    public SFXType sfxClip;
    public SFXType sfxClip2;

    [Header("Music")]
    public MusicType musicClip;
    public MusicType musicClip2;


    // ---------- UI ----------
    public void PlayUI()
    {
        if (MusicManager.Instance == null) return;
        MusicManager.Instance.PlayUI(uiClip);
    }

    // ---------- SFX ----------
    public void PlaySFX()
    {
        if (MusicManager.Instance == null) return;
        MusicManager.Instance.PlaySFX(sfxClip);
    }

    public void PlaySFX2()
    {
        if (MusicManager.Instance == null) return;
        MusicManager.Instance.PlaySFX(sfxClip2);
    }

    // ---------- MUSIC ----------
    public void PlayMusic()
    {
        if (MusicManager.Instance == null) return;
        MusicManager.Instance.PlayMusic(musicClip);
    }
    public void PlayMusic2()
    {
        if (MusicManager.Instance == null) return;
        MusicManager.Instance.PlayMusic(musicClip2);
    }

    public void StopMusic()
    {
        if (MusicManager.Instance == null) return;
        MusicManager.Instance.StopMusic();
    }
}
