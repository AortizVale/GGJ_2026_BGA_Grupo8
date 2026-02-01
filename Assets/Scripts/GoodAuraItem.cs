using UnityEngine;

public class GoodAuraItem : InteractableBase
{
    /*
    [SerializeField] private float rythmToReduce = 0.2f;
    bool hasInteracted = false;
    public override void Interact()
    {
        base.Interact();

        if (hasInteracted) { return; }

        hasInteracted = true;

        GameManager.AuraManager.DecreaseRythm(rythmToReduce);

        if (MusicManager.Instance != null)
        {
            MusicManager.Instance.PlaySFX(SFXType.interaccionCorrecta);
        }
        animator?.SetTrigger("Interact");
        myCollider.enabled = false;
        hideBehindPlayer?.ForceBackground();
    }
    */
}
