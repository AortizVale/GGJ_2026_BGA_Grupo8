using UnityEngine;
public class MusicManager : MonoBehaviour
{
    public static MusicManager Instance { get; private set; }

    [Header("Database")]
    [SerializeField] private MusicDatabase database;

    [Header("Audio Sources")]
    [SerializeField] private AudioSource uiSource;
    [SerializeField] private AudioSource sfxSource;
    [SerializeField] private AudioSource musicSource;

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
}
