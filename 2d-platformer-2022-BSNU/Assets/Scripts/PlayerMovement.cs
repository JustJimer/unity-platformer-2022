using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Header("Movement")]
    [SerializeField] private float speed;
    [SerializeField] private float jumpStrength;
    [SerializeField] private AudioClip jumpSound;
    [Header("Wall Slipping")]
    [SerializeField] private float slipping;
    [SerializeField] private float reboundStrengthSide;
    [SerializeField] private float reboundStrengthUp;
    [Header("Layers")]
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private LayerMask wallLayer;
    [Header("Coyote Time & Multiple Jumps")]
    [SerializeField] private float coyoteTime;
    [SerializeField] private int additionalJumps;

    private Rigidbody2D body;
    private Animator anim;
    private BoxCollider2D boxCollider;

    //private bool isGrounded;
    private float wallJumpDelay;
    private float plaeyrScaleX;
    private float horizontalInput;
    private float coyoteLeft;
    private int jumpsLeft;

    private void Start()
    {
        // getting references from game object
        body = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        boxCollider = GetComponent<BoxCollider2D>();
        plaeyrScaleX = transform.localScale.x;
    }

    private void Update()
    {
        horizontalInput = Input.GetAxis("Horizontal");
        body.velocity = new Vector2(horizontalInput * speed, body.velocity.y);

        // left-right flipping of character
        if (horizontalInput > 0.01f)
            transform.localScale = new Vector3(plaeyrScaleX, transform.localScale.y, transform.localScale.z);
        else if (horizontalInput < -0.01f)
            transform.localScale = new Vector3(-plaeyrScaleX, transform.localScale.y, transform.localScale.z);

/*        if (Input.GetKey(KeyCode.Space))
            Jump();*/

        // animation booleans
        anim.SetBool("isWalking", horizontalInput != 0);
        anim.SetBool("isGrounded", isGrounded());

        /*        // wall jumps
                if (wallJumpDelay > 0.2f)
                {
                    body.velocity = new Vector2(horizontalInput * speed, body.velocity.y);

                    if (onWall() && !isGrounded())
                    {
                        body.gravityScale = slipping;
                        body.velocity = Vector2.zero;
                    }
                    else
                        body.gravityScale = 6.5f;

                    if (Input.GetKey(KeyCode.Space))
                        Jump();
                }
                else
                    wallJumpDelay += Time.deltaTime;*/

        //jumps
        if (Input.GetKeyDown(KeyCode.Space))
            Jump();

        //smooth jump height
        if (Input.GetKeyUp(KeyCode.Space) && body.velocity.y > 0)
            body.velocity = new Vector2(body.velocity.x, body.velocity.y / 2);


        if (onWall())
        {
            body.velocity = Vector2.zero;
            body.gravityScale = slipping;
        }
        else
        {
            body.velocity = new Vector2(horizontalInput * speed, body.velocity.y);
            body.gravityScale = 6.5f;

            if (isGrounded())
            {
                coyoteLeft = coyoteTime; // reset
                jumpsLeft = additionalJumps; // reset
            }
            else
            {
                coyoteLeft -= Time.deltaTime;
            }
        }
    }

    // jump
    private void Jump()
    {
        if (coyoteLeft <= 0 && !onWall() && jumpsLeft <= 0) return;

        SoundLogic.instance.PlaySound(jumpSound);


        if (onWall())
            WallJump();
        else
        {
            if (isGrounded())
                body.velocity = new Vector2(body.velocity.x, jumpStrength);
            else
            {
                if (coyoteLeft > 0)
                    body.velocity = new Vector2(body.velocity.x, jumpStrength);
                else
                {
                    if (jumpsLeft > 0)
                    {
                        body.velocity = new Vector2(body.velocity.x, jumpStrength);
                        jumpsLeft--;
                    }
                }
                coyoteLeft = 0;
            }
        }
    }

    private void WallJump()
    {
        body.AddForce(new Vector2(-Mathf.Sign(transform.localScale.x) * reboundStrengthSide, reboundStrengthSide));
        wallJumpDelay = 0;
    }

    // player can use attack
    public bool attackAvaible()
    {
        return isGrounded() && !onWall() && horizontalInput == 0;
    }

    // player stands on ground
    private bool isGrounded()
    {
        RaycastHit2D raycastHit = Physics2D.BoxCast(boxCollider.bounds.center, boxCollider.bounds.size, 0, Vector2.down, 0.1f, groundLayer);
        return raycastHit.collider != null;
    }
    
    // player near wall
    private bool onWall()
    {
        RaycastHit2D raycastHit = Physics2D.BoxCast(boxCollider.bounds.center, boxCollider.bounds.size, 0, new Vector2(transform.localScale.x, 0), 0.1f, wallLayer);
        return raycastHit.collider != null;
    }

}
