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

    [SerializeField] private float initialTime = 100f;
    [SerializeField] private PlayerCharacter playerCharacter = default;
    [SerializeField] private float currentTime;
    [Header("Idle Time Control")]
    [SerializeField] private float slowDownDuration = 2.5f;
    [SerializeField] private float pauseDuration = 5f;
    [SerializeField] private float speedUpDuration = 2.5f;

    private float idleTimer;
    private float idleSpeedMultiplier = 1f;

    private bool isCountdownRunning;

    private void Start()
    {
        StartCountdown();
    }
    public void StartCountdown()
    {
        currentTime = initialTime;
        isCountdownRunning = true;
    }

    private void Update()
    {
        if (!isCountdownRunning || playerCharacter == null)
            return;

        float finalMultiplier;

        if (!playerCharacter.IsMoving)
        {
            idleTimer += Time.deltaTime;

            // FASE 1 — desacelerar
            if (idleTimer <= slowDownDuration)
            {
                float t = idleTimer / slowDownDuration;
                idleSpeedMultiplier = Mathf.Lerp(1f, 0f, t);
            }
            // FASE 2 — tiempo congelado
            else if (idleTimer <= slowDownDuration + pauseDuration)
            {
                idleSpeedMultiplier = 0f;
            }
            // FASE 3 — volver a subir (castigo)
            else
            {
                float t = (idleTimer - slowDownDuration - pauseDuration) / speedUpDuration;
                idleSpeedMultiplier = Mathf.Lerp(0f, 1f, t);
            }

            finalMultiplier = idleSpeedMultiplier;
        }
        else
        {
            // El jugador se mueve → reset total
            idleTimer = 0f;
            idleSpeedMultiplier = 1f;
            finalMultiplier = playerCharacter.CurrentSpeedMultiplier;
        }

        currentTime -= Time.deltaTime * finalMultiplier;
        currentTime = Mathf.Max(currentTime, 0f);

        playerCharacter.SetBodyAlpha(currentTime / initialTime);

        if (currentTime <= 0f)
        {
            isCountdownRunning = false;
            OnTimeEnded();
        }
    }


    private void OnTimeEnded()
    {
        Debug.Log("Tiempo agotado");
        // Game Over, evento, lo que necesites
    }



}
