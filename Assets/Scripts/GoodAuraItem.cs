using UnityEngine;

public class GoodAuraItem : InteractableBase
{
    [SerializeField] private float rythToReduce = 0.2f;
    public override void Interact()
    {
        base.Interact();
        GameManager.AuraManager.DecreaseRythm(rythToReduce);

        if (MusicManager.Instance != null)
        {
            MusicManager.Instance.PlaySFX(SFXType.interaccionCorrecta);
        }
        Destroy(gameObject);
    }
}
