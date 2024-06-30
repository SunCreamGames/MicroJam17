using System;
using UnityEditor;
using UnityEngine;
using UnityEngine.Events;

public class CharacterController2D : MonoBehaviour
{
    [SerializeField] private float JumpForce = 400f;
    [Range(0, 1)][SerializeField] private float CrouchSpeed = .36f;
    [Range(0, .3f)][SerializeField] private float MovementSmoothing = .05f;
    [SerializeField] private LayerMask WhatIsGround;
    [SerializeField] private Transform GroundCheck;
    [SerializeField] private Transform[] CeilingChecks;
    [SerializeField] private Collider2D CrouchDisableCollider;

    const float GroundedRadius = .2f;
    public Animator animator;
    [HideInInspector]
    public bool Grounded;
    const float CeilingRadius = .05f;
    const float CeilingAdditionalPointsDistance = .3f;
    private Rigidbody2D Rigidbody2D;
    private bool FacingRight = true;
    private Vector3 Velocity = Vector3.zero;

    FruitObject interactingObject;


    [SerializeField]
    Player player;
    public event Action<float> OnFlight;
    public event Action OnJump;

    private bool isInteracting;

    [Header("Events")]
    [Space]

    public UnityEvent OnLandEvent;

    private void Awake()
    {
        Rigidbody2D = GetComponent<Rigidbody2D>();

        if (OnLandEvent == null)
            OnLandEvent = new UnityEvent();

        isInteracting = false;
    }

    private void FixedUpdate()
    {
        bool wasGrounded = Grounded;
        Grounded = false;

        Collider2D[] colliders = Physics2D.OverlapCircleAll(GroundCheck.position, GroundedRadius, WhatIsGround);
        for (int i = 0; i < colliders.Length; i++)
        {
            if (colliders[i].gameObject != gameObject)
            {
                Grounded = true;
                if (!wasGrounded)
                {
                    OnLandEvent.Invoke();
                    animator.SetBool("Grounded", true);
                }
            }
        }
    }

    public bool TryEat(FruitObject fruit)
    {
        isInteracting = true;
        interactingObject = fruit;
        var isFruitEaten = fruit.InteractWith(Time.fixedDeltaTime);

        if (isFruitEaten)
        {
            isInteracting = false;
            interactingObject = null;
        }

        return isFruitEaten;
    }

    public void StopEating()
    {
        isInteracting = false;
        if (interactingObject != null)
            interactingObject.StopInteraction();
    }

    public void Move(float move, float flyMove, bool crouch, bool jump)
    {

        if (isInteracting)
        {
            Rigidbody2D.velocity = Vector3.zero;
            player.StaminaRegenStart(); // StartRegen if we are interacting and not moving
            return;
        }

        if (!crouch)
        {
            if (Physics2D.OverlapCircle(CeilingChecks[0].position, CeilingRadius, WhatIsGround) ||
                Physics2D.OverlapCircle(CeilingChecks[1].position, CeilingRadius, WhatIsGround) ||
                Physics2D.OverlapCircle(CeilingChecks[2].position, CeilingRadius, WhatIsGround))
            {
                crouch = true;
                flyMove = 0f;
            }
        }

        if (Grounded && crouch)
        {
            flyMove = 0f;
            move *= CrouchSpeed;

            if (CrouchDisableCollider != null)
                CrouchDisableCollider.enabled = false;
        }
        else
        {
            if (CrouchDisableCollider != null)
                CrouchDisableCollider.enabled = true;
        }
        animator.SetFloat("moveX", Mathf.Abs(move));
        animator.SetFloat("moveY", Mathf.Abs(flyMove));


        Vector3 targetVelocity = new Vector2(move * 10f, Rigidbody2D.velocity.y);
        if (flyMove > 0f && player.CanFlight && (!Grounded || player.CanStartFlight))
        {
            targetVelocity = new Vector2(move * 10f, flyMove * 10f);
            player.UseStamina(Time.fixedDeltaTime);
        }
        animator.SetBool("YUp", targetVelocity.y > 0);

        if (Grounded && move == 0f)
            player.StaminaRegenStart(); // StartRegen if we are standing/crouching on the same spot
        else
            player.StaminaRegenStop();


        Rigidbody2D.velocity = Vector3.SmoothDamp(Rigidbody2D.velocity, targetVelocity, ref Velocity, MovementSmoothing);

        if (move > 0 && !FacingRight)
        {
            Flip();
        }
        else if (move < 0 && FacingRight)
        {
            Flip();
        }
        animator.SetBool("Crouch", crouch);


        if (Grounded && jump && player.CanPerformJump)
        {
            player.StaminaRegenStop();
            animator.SetBool("Grounded", Grounded);
            Grounded = false;
            player.UseStamina(player.jumpStaminaAmount);
            Rigidbody2D.AddForce(new Vector2(0f, JumpForce));
            animator.SetTrigger("Jump");
        }

        if (!Grounded)
            animator.SetBool("Grounded", Grounded);

    }


    private void Flip()
    {
        FacingRight = !FacingRight;

        Vector3 theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;
    }

}