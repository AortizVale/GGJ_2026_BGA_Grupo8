using UnityEngine;

public class BadAuraItem : InteractableBase
{
    [SerializeField] private float rythmToIncrease = 0.2f;
    bool hasInteracted = false;
    public override void Interact()
    {
        base.Interact();

        if (hasInteracted) { return; }

        hasInteracted = true;

        GameManager.AuraManager.IncreaseItemRythm(rythmToIncrease);
        if (MusicManager.Instance != null)
        {
            MusicManager.Instance.PlaySFX(SFXType.interaccionIncorrecta);
        }
        animator?.SetTrigger("Interact");
        myCollider.enabled = false;
        hideBehindPlayer?.ForceBackground();
    }
}
