using UnityEngine;

public class AuraManager : MonoBehaviour
{
    [Range(0f, 1f)]
    [SerializeField] private float transparency;
    [Range(0f, 5f)]
    [SerializeField] private float itemsRythm = 1;

    [Header("Time")]
    [SerializeField] private float initialTime = 100f;
    [SerializeField] private float currentTime;
    [Header("Idle Time Control")]
    [SerializeField] private float slowDownDuration = 2.5f;
    [SerializeField] private float pauseDuration = 5f;
    [SerializeField] private float speedUpDuration = 2.5f;

    private float idleTimer;
    private float idleSpeedMultiplier = 1f;
    
    private bool isCountdownRunning;
    private float walkingRythm;
    public float Rythm => walkingRythm+itemsRythm;
    
    public void StartCountdown()
    {
        currentTime = initialTime;
        isCountdownRunning = true;
    }

    private void Update()
    {
        if (!isCountdownRunning || GameManager.PlayerCharacter == null)
            return;


        if (!GameManager.PlayerCharacter.IsMoving)
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

            walkingRythm = idleSpeedMultiplier;
        }
        else
        {
            // El jugador se mueve → reset total
            idleTimer = 0f;
            idleSpeedMultiplier = 1f;

            walkingRythm = GameManager.PlayerCharacter.IsSprinting ? 2f : 1f; ;
        }

        currentTime -= Time.deltaTime * Rythm;
        currentTime = Mathf.Max(currentTime, 0f);

        transparency = Mathf.Clamp01(currentTime /initialTime);

        GameManager.PlayerCharacter.SetBodyAlpha(transparency);
        GameManager.PlayerCharacter.SetLightRadius(Mathf.Clamp(2-itemsRythm, 0.2f, 2f));

        if (currentTime <= 0f)
        {
            isCountdownRunning = false;
            OnTimeEnded();
        }
    }

    public void IncreaseItemRythm(float value)
    {
        itemsRythm = Mathf.Max(0, itemsRythm + value);
    }

    public void DecreaseRythm(float value)
    {
        itemsRythm = Mathf.Max(0, itemsRythm - value);
    }
    private void OnTimeEnded()
    {
        Debug.Log("Tiempo agotado");
        // Game Over, evento, lo que necesites
    }
}
