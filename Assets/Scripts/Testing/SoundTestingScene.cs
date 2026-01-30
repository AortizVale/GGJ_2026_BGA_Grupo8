using UnityEngine;

public class SoundTestingScene : MonoBehaviour
{
    [Header("UI")]
    public UIClipType uiClip;

    [Header("SFX")]
    public SFXType sfxClip;

    [Header("Music")]
    public MusicType musicClip;

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

    // ---------- MUSIC ----------
    public void PlayMusic()
    {
        if (MusicManager.Instance == null) return;
        MusicManager.Instance.PlayMusic(musicClip);
    }

    public void StopMusic()
    {
        if (MusicManager.Instance == null) return;
        MusicManager.Instance.StopMusic();
    }
}
