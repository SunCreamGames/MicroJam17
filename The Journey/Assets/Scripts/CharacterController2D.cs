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
    [SerializeField] private SpriteRenderer CrouchDisableImage;

    const float GroundedRadius = .2f;
    private bool Grounded;
    const float CeilingRadius = .05f;
    const float CeilingAdditionalPointsDistance = .3f;
    private Rigidbody2D Rigidbody2D;
    private bool FacingRight = true;
    private Vector3 Velocity = Vector3.zero;

    [Header("Events")]
    [Space]

    public UnityEvent OnLandEvent;

    private void Awake()
    {
        Rigidbody2D = GetComponent<Rigidbody2D>();

        if (OnLandEvent == null)
            OnLandEvent = new UnityEvent();

        OnLandEvent.AddListener(() => { Debug.Log($"<color=cyan> Landed </color>"); });

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
                    OnLandEvent.Invoke();
            }
        }
    }


    public void Move(float move, float flyMove, bool crouch, bool jump)
    {
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
            {
                CrouchDisableCollider.enabled = false;
                CrouchDisableImage.enabled = false;
            }
        }
        else
        {
            if (CrouchDisableCollider != null)
            {
                CrouchDisableCollider.enabled = true;
                CrouchDisableImage.enabled = true;
            }
        }

        Vector3 targetVelocity = new Vector2(move * 10f, Rigidbody2D.velocity.y);
        if (flyMove > 0f)
        {
            targetVelocity = new Vector2(move * 10f, flyMove * 10f);
        }

        Rigidbody2D.velocity = Vector3.SmoothDamp(Rigidbody2D.velocity, targetVelocity, ref Velocity, MovementSmoothing);

        if (move > 0 && !FacingRight)
        {
            Flip();
        }
        else if (move < 0 && FacingRight)
        {
            Flip();
        }


        if (Grounded && jump)
        {
            Grounded = false;
            Rigidbody2D.AddForce(new Vector2(0f, JumpForce));
        }
    }


    private void Flip()
    {
        FacingRight = !FacingRight;

        Vector3 theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;
    }
}