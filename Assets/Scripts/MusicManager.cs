using System.Collections.Generic;
using UnityEngine;

using DG.Tweening;


public class MusicManager : MonoBehaviour
{
    public static MusicManager Instance { get; private set; }

    [Header("Database")]
    [SerializeField] private MusicDatabase database;

    [Header("Audio Sources")]
    [SerializeField] private AudioSource uiSource;
    [SerializeField] private AudioSource sfxSource;
    [SerializeField] private AudioSource musicSource;
    [SerializeField] private AudioSource heartBeatAudioSource;
    [Header("Heartbeat")]
    [SerializeField] private float heartbeatFadeTime = 0.25f;
    private Tween heartbeatTween;
    private List<SFXType> heartbeatSfxList = new List<SFXType> { SFXType.Latido1, SFXType.Latido2, SFXType.Latido3};
    private SFXType currentHeartbeat;


    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    public void PlayUI(UIClipType type)
    {
        var clip = database.GetUIClip(type);
        if (clip != null)
            uiSource.PlayOneShot(clip);
    }

    public void PlaySFX(SFXType type)
    {
        var clip = database.GetSFXClip(type);
        if (clip != null)
            sfxSource.PlayOneShot(clip);
    }

    public void PlayMusic(MusicType type, bool loop = true)
    {
        var clip = database.GetMusicClip(type);
        if (clip == null)
            return;

        if (musicSource.clip == clip)
            return;

        musicSource.clip = clip;
        musicSource.loop = loop;
        musicSource.Play();
    }

    public void StopMusic()
    {
        musicSource.Stop();
    }

    public bool IsHeartbeatPlayingByValue(float value)
    {
        return GetHeartbeatByValue(value) == currentHeartbeat;
    }
    private SFXType GetHeartbeatByValue(float value)
    {
        if (heartbeatSfxList == null || heartbeatSfxList.Count == 0)
            return SFXType.Undefined;

        value = Mathf.Clamp01(value);

        int lastIndex = heartbeatSfxList.Count - 1;
        int index = Mathf.RoundToInt(value * lastIndex);

        return heartbeatSfxList[index];
    }
    public void PlayHeartbeatByValue(float value)
    {
        SFXType sfxType = GetHeartbeatByValue(value);
        if (sfxType == SFXType.Undefined)
            return;

        if (currentHeartbeat == sfxType)
            return;

        AudioClip clip = database.GetSFXClip(sfxType);
        if (clip == null)
            return;

        currentHeartbeat = sfxType;

        //  Detener tween anterior
        heartbeatTween?.Kill();

        heartbeatTween = DOTween.Sequence()
            .Append(heartBeatAudioSource.DOFade(0.5f, heartbeatFadeTime))
            .AppendCallback(() =>
            {
                heartBeatAudioSource.clip = clip;
                heartBeatAudioSource.loop = true;
                heartBeatAudioSource.Play();
            })
            .Append(heartBeatAudioSource.DOFade(1f, heartbeatFadeTime));
    }

    public void PlayDeathHeartBeat()
    {
        AudioClip clip = database.GetSFXClip(SFXType.LatidoConMuerte);
        if (clip == null)
            return;

        if (currentHeartbeat == SFXType.LatidoConMuerte)
            return;

        currentHeartbeat = SFXType.LatidoConMuerte;

        heartbeatTween?.Kill();

        heartbeatTween = DOTween.Sequence()
            .Append(heartBeatAudioSource.DOFade(0.5f, heartbeatFadeTime))
            .AppendCallback(() =>
            {
                heartBeatAudioSource.clip = clip;
                heartBeatAudioSource.loop = false;
                heartBeatAudioSource.Play();
            })
            .Append(heartBeatAudioSource.DOFade(1f, heartbeatFadeTime));
    }


}
