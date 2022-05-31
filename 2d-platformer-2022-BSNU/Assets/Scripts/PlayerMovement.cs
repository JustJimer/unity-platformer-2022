using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float speed;
    private Rigidbody2D body;
    private Animator anim;
    private bool isGrounded;

    private void Awake()
    {
        // getting references from game object
        body = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }

    private void Update()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        body.velocity = new Vector2(horizontalInput * speed, body.velocity.y);

        // left-right flipping of character
        if (horizontalInput > 0.01f)
            transform.localScale = new Vector3(1.6f, 1.6f, 1.6f);
        else if (horizontalInput < -0.01f)
            transform.localScale = new Vector3(-1.6f, 1.6f, 1.6f);

        if (Input.GetKey(KeyCode.Space) && isGrounded)
            Jump();

        // animation booleans
        anim.SetBool("isWalking", horizontalInput != 0);
        anim.SetBool("isGrounded", isGrounded);
    }

    private void Jump()
    {
        body.velocity = new Vector2(body.velocity.x, speed);
        isGrounded = false;
        anim.SetTrigger("jump");
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Ground")
            isGrounded = true;
    }

}