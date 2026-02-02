using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }
    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
    } 

    
    [SerializeField] private PlayerCharacter playerCharacter = default;
    [SerializeField] private AuraManager auraManager = default;

    [SerializeField] private CalmCanvas calmCanvas = default;
    public static PlayerCharacter PlayerCharacter => Instance.playerCharacter;

    public static AuraManager AuraManager => Instance.auraManager;
    public static CalmCanvas CalmCanvas => Instance.calmCanvas;
    private void Start()
    {
        calmCanvas.OpenHUD();
        MusicManager.Instance.PlayMusic(MusicType.Ambiente1);
        AuraManager.StartCountdown();
    }

}
