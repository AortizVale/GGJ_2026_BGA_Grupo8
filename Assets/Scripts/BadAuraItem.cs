using UnityEngine;

public class BadAuraItem : InteractableBase
{
    [SerializeField] private float rythToIncrease = 0.2f;
    public override void Interact()
    {
        base.Interact();
        GameManager.AuraManager.IncreaseItemRythm(0.2f);
        if (MusicManager.Instance != null)
        {
            MusicManager.Instance.PlaySFX(SFXType.interaccionIncorrecta);
        }
        Destroy(gameObject);
    }
}
