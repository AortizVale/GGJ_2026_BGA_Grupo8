using UnityEngine;

public class AuraItem : InteractableBase
{
    [SerializeField] private float rythmToAdd = 0.2f;
    [SerializeField] private bool disableColliderAfterInteract;
    bool hasInteracted = false;
    
    public override void Interact()
    {
        base.Interact();

        if (hasInteracted) { return; }

        hasInteracted = true;

        GameManager.AuraManager.AddItemRythm(rythmToAdd);

        if (MusicManager.Instance != null)
        {
            if (rythmToAdd > 0) 
            {
                MusicManager.Instance.PlaySFX(SFXType.interaccionIncorrecta);
            }
            else
            {
                MusicManager.Instance.PlaySFX(SFXType.interaccionCorrecta);
            }
                
        }
        animator?.SetTrigger("Interact");
        myCollider.enabled = !disableColliderAfterInteract;
        if (disableColliderAfterInteract)
        {
            hideBehindPlayer?.ForceBackground();
        }
    }
}