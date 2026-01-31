using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerCharacter: MonoBehaviour
{

    [SerializeField] InputActionReference move;
    [SerializeField] InputActionReference interact; 
    [SerializeField] InputActionReference sprint;
    bool isSprinting;
    [SerializeField] float sprintMultiplier = 1.5f;

    [SerializeField] float linearSpeed = 1f;
    [Header("Interact data")]
    [SerializeField] float interactRadius = 0.3f;
    [SerializeField] float interactRange = 1f;
    Animator animator;
    Rigidbody2D rb2D;

    Vector2 lastMoveDirection;
    Vector2 rawMove;
    Vector2 interactDirection = Vector2.down;
    bool mustInteract;

    private void OnEnable()
    {
        move.action.Enable();
        move.action.started += OnMove;
        move.action.performed += OnMove;
        move.action.canceled += OnMove;

        interact.action.Enable();
        interact.action.performed += OnInteract;

        sprint.action.Enable();
        sprint.action.started += OnSprint;
        sprint.action.canceled += OnSprint;

    }

    private void OnDisable()
    {
        move.action.Disable();
        move.action.started -= OnMove;
        move.action.performed -= OnMove;
        move.action.canceled -= OnMove;

        interact.action.Disable();
        interact.action.performed -= OnInteract;

        sprint.action.Disable();
        sprint.action.started -= OnSprint;
        sprint.action.canceled -= OnSprint;

    }

    protected virtual void Awake()
    {
        rb2D = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    protected virtual void Update()
    {
        animator.SetFloat("HorizontalVelocity", lastMoveDirection.x);
        animator.SetFloat("VerticalVelocity", lastMoveDirection.y);

        if (mustInteract)
        {
            mustInteract = false;
            PerformInteraction();
        }
    }

    private void FixedUpdate()
    {
        Move(rawMove);
    }


    private void OnMove(InputAction.CallbackContext context)
    {
        rawMove = context.action.ReadValue<Vector2>();
        if (rawMove.magnitude > 0f)
        {
            interactDirection = rawMove.normalized;
        }
    }


    protected void Move(Vector2 direction)
    {
        float speed = isSprinting ? linearSpeed * sprintMultiplier : linearSpeed;

        rb2D.linearVelocity = direction * speed;

        if (direction.sqrMagnitude > 0.01f)
            lastMoveDirection = direction;
    }




    private void PerformInteraction()
    {
        RaycastHit2D[] hits =  Physics2D.CircleCastAll(
        transform.position,
        interactRadius,
        interactDirection,
        interactRange);

        foreach (RaycastHit2D hit in hits)
        {
            
            if (hit.collider.gameObject != this.gameObject)
            {
                animator.SetTrigger("Interact");
                InteractableBase otherInteractableBase = hit.collider.GetComponent<InteractableBase>();

                otherInteractableBase?.Interact();
            }
        }
    }

    //Dibujar gizmos de golpe
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Vector3 center = transform.position + (Vector3)(interactDirection * interactRange);
        Gizmos.DrawWireSphere(center, interactRadius);
    }


    private void OnInteract(InputAction.CallbackContext context)
    {
        mustInteract = true;
    }

    private void OnSprint(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            isSprinting = true;
        }
        else if (context.canceled)
        {
            isSprinting = false;
        }
        animator.SetBool("IsSprinting", isSprinting);

    }

}
