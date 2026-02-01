using DG.Tweening;
using UnityEditor.Rendering;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Rendering.Universal;
using UnityEngine.U2D;

public class PlayerCharacter: MonoBehaviour
{

    [SerializeField] InputActionReference move;
    [SerializeField] InputActionReference interact; 
    [SerializeField] InputActionReference sprint;
    
    bool isSprinting;
    [SerializeField] float sprintMultiplier = 1.5f;
    [SerializeField] SpriteRenderer bodySpriteRenderer;
    [SerializeField] float linearSpeed = 1f;
    [SerializeField] Animator maskAnimator;
    [SerializeField] SpriteRenderer maskSpriteRenderer;
    [SerializeField] Light2D auraLight;
    [Header("Interact data")]
    [SerializeField] float interactRadius = 0.3f;
    [SerializeField] float interactRange = 1f;

    bool inputsEnabled = true;
    Animator animator;
    Rigidbody2D rb2D;

    Vector2 lastMoveDirection;
    Vector2 rawMove;
    Vector2 interactDirection = Vector2.down;
    bool mustInteract;

    public bool IsSprinting => isSprinting;
    public bool IsMoving => rawMove.sqrMagnitude > 0.01f;


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

        maskAnimator.SetFloat("HorizontalVelocity", lastMoveDirection.x);
        maskAnimator.SetFloat("VerticalVelocity", lastMoveDirection.y);

        if (!inputsEnabled) { return; }
        if (mustInteract)
        {
            mustInteract = false;
            PerformInteraction();
        }
    }

    private void FixedUpdate()
    {
        if (!inputsEnabled) { return; }
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
        if (!inputsEnabled) { return; }
        float speed = isSprinting ? linearSpeed * sprintMultiplier : linearSpeed;

        Vector2 finalDir = ApplyIsometricWithTolerance(direction);

        rb2D.linearVelocity = finalDir * speed;

        bool isMoving = direction.sqrMagnitude > 0.01f;
        if (isMoving)
            lastMoveDirection = direction; // input crudo


        if (animator.GetBool("IsMoving") != isMoving)
        {
            animator.SetBool("IsMoving", isMoving);
        }

        if (maskAnimator.GetBool("IsMoving") != isMoving)
        {
            maskAnimator.SetBool("IsMoving", isMoving);
        }
    }




    private void PerformInteraction()
    {
        if (!inputsEnabled) { return; }
        RaycastHit2D[] hits =  Physics2D.CircleCastAll(
        transform.position,
        interactRadius,
        interactDirection,
        interactRange);

        foreach (RaycastHit2D hit in hits)
        {
            
            if (hit.collider.gameObject != this.gameObject)
            {
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
        maskAnimator.SetBool("IsSprinting", isSprinting);

    }

    public void SetBodyAlpha(float alpha) 
    {
        Color color = bodySpriteRenderer.color;
        color.a = Mathf.Clamp01(alpha); // 0 = invisible, 1 = totalmente visible
        bodySpriteRenderer.color = color;
    }

    public void SetLightRadius(float radius)
    {
        auraLight.pointLightOuterRadius = radius;
    }

    [SerializeField] float isoReferenceAngle = 30f;

    Vector2 ApplyIsometricWithTolerance(Vector2 input)
    {
        if (input.sqrMagnitude < 0.001f)
            return Vector2.zero;

        input = input.normalized;

        float angle = Mathf.Atan2(input.y, input.x) * Mathf.Rad2Deg;
        if (angle < 0) angle += 360f;

        const float halfTolerance = 22.5f;

        // ---- Ejes puros (±22.5°) ----
        if (angle >= 360f - halfTolerance || angle <= halfTolerance)
            return Vector2.right;

        if (angle >= 90f - halfTolerance && angle <= 90f + halfTolerance)
            return Vector2.up;

        if (angle >= 180f - halfTolerance && angle <= 180f + halfTolerance)
            return Vector2.left;

        if (angle >= 270f - halfTolerance && angle <= 270f + halfTolerance)
            return Vector2.down;

        // ---- Diagonales isométricas fijas (30° reales) ----
        float isoAngle;

        if (angle > halfTolerance && angle < 90f - halfTolerance)
            isoAngle = isoReferenceAngle;          // ↗
        else if (angle > 90f + halfTolerance && angle < 180f - halfTolerance)
            isoAngle = 180f - isoReferenceAngle;         // ↖
        else if (angle > 180f + halfTolerance && angle < 270f - halfTolerance)
            isoAngle = 180f + isoReferenceAngle;         // ↙
        else
            isoAngle = 360f - isoReferenceAngle;         // ↘

        float rad = isoAngle * Mathf.Deg2Rad;
        return new Vector2(Mathf.Cos(rad), Mathf.Sin(rad));
    }

    public void OnDeath()
    {
        inputsEnabled = false;
        animator.SetTrigger("Die");
        maskAnimator.SetTrigger("Die");
        rb2D.linearVelocity = Vector2.zero;
        FadeMaskAndShrinkLight(0.06f, 1f, 0.2f, 1f, 2f, 0.1f, 0.5f);
    }

    private void FadeMaskAndShrinkLight(
    float targetHeightY,
    float moveHeightTime,

    float outerRadiusTarget,
    float outerRadiusTime,

    float spriteFadeTime,

    float innerShrinkTime,
    float finalOuterShrinkTime
)
    {
        Sequence seq = DOTween.Sequence();

        Transform lightTransform = auraLight.transform;

        // 0️⃣ Mover altura (posición Y)
        seq.Append(
            lightTransform.DOLocalMoveY(targetHeightY, moveHeightTime)

        );

        // 1️⃣ Baja radio externo (primer cierre)
        seq.Append(
            DOTween.To(
                () => auraLight.pointLightOuterRadius,
                x => auraLight.pointLightOuterRadius = x,
                outerRadiusTarget,
                outerRadiusTime
            )
        );

        // 2️⃣ Fade out del sprite
        seq.Append(maskSpriteRenderer.DOFade(0f, spriteFadeTime));

        // 3️⃣ Baja radio interno
        seq.Append(
            DOTween.To(
                () => auraLight.pointLightInnerRadius,
                x => auraLight.pointLightInnerRadius = x,
                0f,
                innerShrinkTime
            )
        );

        // 4️⃣ Baja radio externo hasta 0
        seq.Append(
            DOTween.To(
                () => auraLight.pointLightOuterRadius,
                x => auraLight.pointLightOuterRadius = x,
                0f,
                finalOuterShrinkTime
            )
        );
    }


}
