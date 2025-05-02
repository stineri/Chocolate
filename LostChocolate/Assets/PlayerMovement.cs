using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    // Movement speed
    public float speed = 5f;
    public float runSpeed = 10f;  // running speed
    public float crouchSpeed = 2f; // crouching speed
    public float jumpForce = 5f;

    Rigidbody2D rb;

    bool grounded;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        float hAxis = Input.GetAxis("Horizontal");

        if (Input.GetKey(KeyCode.LeftControl))
        {
            // Crouch: move slower
            rb.linearVelocity = new Vector2(hAxis * crouchSpeed, rb.linearVelocity.y);
        }
        else if (Input.GetKey(KeyCode.LeftShift))
        {
            // Run: move faster
            rb.linearVelocity = new Vector2(hAxis * runSpeed, rb.linearVelocity.y);
        }
        else
        {
            // Normal walk speed
            rb.linearVelocity = new Vector2(hAxis * speed, rb.linearVelocity.y);
        }

        if (Input.GetKeyDown(KeyCode.Space) && grounded)
        {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpForce);
        }
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Ground"))
        {
            grounded = true;
        }
    }

    private void OnCollisionExit2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Ground"))
        {
            grounded = false;
        }

    }
}