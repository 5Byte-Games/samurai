using System;
using UnityEngine;

// gets unity engine input and sets v2 and v3 to standard usage
using Input = UnityEngine.Input;
using Vector2 = UnityEngine.Vector2;
using Vector3 = UnityEngine.Vector3;

public class PlayerMovement : MonoBehaviour
{
    // gets rigidbody, boxcollider, animator and sprite renderer
    private Rigidbody2D rb;
    private BoxCollider2D coll;
    private Animator anim;
    private SpriteRenderer sprite;

    // gets layermask for jumpable ground
    [SerializeField] private LayerMask jumpableGround;
    [SerializeField] private LayerMask slideableWall;
    [SerializeField] private Transform wallCheck;

    // sets direction
    private float dirX = 0f;

    // player settings
    [SerializeField] private float playerJumpHeight = 8f;
    [SerializeField] private float playerSpeed = 10f;
    private bool isWallSliding;
    public bool IsGrounded { get; set;}
    private float wallSlidingSPeed = 2f;

    private bool isFacingRight = true;

    // sets enum for multiple movement states
    private enum movementState { idle, running, jumping, falling};

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        coll = GetComponent<BoxCollider2D>();
        //anim = GetComponent<Animator>();
        sprite = GetComponent<SpriteRenderer>();

        WallSlide();
    }

    private void Update()
    {

        // calls player movement
        playerMovement();

        // calls wall slide
        WallSlide();

        // checks direction player is facing
        Flip();
    }

    private void playerMovement()
    {
        // get direction X axis
        dirX = Input.GetAxisRaw("Horizontal");

        // apply X axis to playerspeed
        rb.velocity = new Vector3(dirX * playerSpeed, rb.velocity.y);

        if (Input.GetButtonDown("Jump") && isGrounded())
        {
            rb.velocity = new Vector2(rb.velocity.x, playerJumpHeight);
        }

        if (Input.GetButtonUp("Jump") && rb.velocity.y > 0f)
        {
            rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y * 0.5f);
        }
    }

    private void WallSlide()
    {
        if (isWalled() && !isGrounded() && dirX != 0f)
        {
            isWallSliding = true;
            rb.velocity = new Vector2(rb.velocity.x, Mathf.Clamp(rb.velocity.y, -wallSlidingSPeed, float.MaxValue));
        }
        else
        {
            isWallSliding = false;
        }
    }

    private bool isWalled()
    {
        return Physics2D.OverlapCircle(wallCheck.position, 0.2f, slideableWall);
    }

    // checks if player is grounded
    private bool isGrounded()
    {
        // makes boxcast of current hitbox, shifts it down a small amount and looks for collision with jumpable ground
        if (Physics2D.BoxCast(coll.bounds.center, coll.bounds.size, 0f, Vector2.down, .1f, jumpableGround) == true)
        {
            IsGrounded = true;
        } else
        {
            IsGrounded = false;
        }
        return IsGrounded;
    }

    public bool GetIsFacingRight()
    {
        if (isFacingRight && dirX < 0f || !isFacingRight && dirX > 0f)
        {
            isFacingRight = !isFacingRight;
        }
        return isFacingRight;
    }

    private void Flip()
    {
        if (isFacingRight && dirX < 0f || !isFacingRight && dirX > 0f)
        {
            isFacingRight = !isFacingRight;
            Vector3 localScale = transform.localScale;
            localScale.x *= -1f;
            transform.localScale = localScale;
        }
    }
}