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
        DontDestroyOnLoad(gameObject);
    } 

    
    [SerializeField] private PlayerCharacter playerCharacter = default;
    [SerializeField] private AuraManager auraManager = default;
    public static PlayerCharacter PlayerCharacter => Instance.playerCharacter;

    public static AuraManager AuraManager => Instance.auraManager;

    private void Start()
    {
        AuraManager.StartCountdown();
    }

}
