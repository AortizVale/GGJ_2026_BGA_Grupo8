using UnityEngine;

public class InteractableBase : MonoBehaviour
{
    public virtual void Interact()
    {
        Debug.Log("Interacting with " + gameObject.name);
    }
}
