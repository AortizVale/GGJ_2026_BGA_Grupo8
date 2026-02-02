using UnityEngine;

public class Forest : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        MusicManager.Instance.PlayMusic(MusicType.BackgroundMusic);
    }
}
