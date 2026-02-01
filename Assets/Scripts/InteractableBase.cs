using UnityEngine;

public class InteractableBase : MonoBehaviour
{
    [SerializeField] protected Animator animator = default;
    [SerializeField] protected Collider2D myCollider = default;
    [SerializeField] protected HideBehindPlayer hideBehindPlayer = default;
    public virtual void Interact()
    {
        Debug.Log("Interacting with " + gameObject.name);
    }


}
