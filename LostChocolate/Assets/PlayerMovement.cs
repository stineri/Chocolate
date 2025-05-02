using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    // Movement speed
    public float speed = 5f;
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
        rb.linearVelocity = new Vector2(hAxis * speed, rb.linearVelocity.y);

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