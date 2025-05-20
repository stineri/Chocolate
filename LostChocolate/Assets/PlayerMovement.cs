using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float speed = 5f;
    public float runSpeed = 10f;
    public float crouchSpeed = 2f;
    public float jumpForce = 10f;

    private Rigidbody2D rb;
    private bool grounded = false;

    private bool isStunned = false;
    private float stunTimer = 0f;
    private bool isFacingRight = true;

    public Collider2D normalCollider;
    public Collider2D stunCollider;

    [SerializeField] private Animator animator;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        Collider2D[] colliders = GetComponents<Collider2D>();
        normalCollider = colliders[0];
        stunCollider = colliders[1];

        EnableNormalCollider(); // Start with normal collider active
    }

    void Update()
    {
        // Handle stun logic
        if (isStunned)
        {
            stunTimer -= Time.deltaTime;
            rb.linearVelocity = new Vector2(0f, rb.linearVelocity.y); // Stop horizontal movement, allow falling

            animator.SetBool("isStunned", true);

            if (stunTimer <= 0f)
            {
                isStunned = false;
                animator.SetBool("isStunned", false);
                EnableNormalCollider(); // Switch back to normal collider when stun ends
            }

            return;
        }

        float hAxis = Input.GetAxis("Horizontal");
        flip();

        // Handle movement
        if (Input.GetKey(KeyCode.LeftControl))
        {
            rb.linearVelocity = new Vector2(hAxis * crouchSpeed, rb.linearVelocity.y);
        }
        else if (Input.GetKey(KeyCode.LeftShift))
        {
            rb.linearVelocity = new Vector2(hAxis * runSpeed, rb.linearVelocity.y);
        }
        else
        {
            rb.linearVelocity = new Vector2(hAxis * speed, rb.linearVelocity.y);
        }

        // Handle jump
        if (Input.GetKeyDown(KeyCode.Space) && grounded)
        {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpForce);
            grounded = false;
        }

        animator.SetBool("isRunning", hAxis != 0);
    }

    public void EnableStunCollider()
    {
        normalCollider.enabled = false;
        stunCollider.enabled = true;
    }

    public void EnableNormalCollider()
    {
        stunCollider.enabled = false;
        normalCollider.enabled = true;
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Ground"))
        {
            grounded = true;
            Debug.Log("Player is grounded.");
        }
    }

    private void OnCollisionExit2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Ground"))
        {
            grounded = false;
            Debug.Log("Player is no longer grounded.");
        }
    }

    public void Stun(float duration)
    {
        Debug.Log("Player got stunned!");
        isStunned = true;
        stunTimer = duration;
        rb.linearVelocity = Vector2.zero;
        animator.SetBool("isStunned", true);
        EnableStunCollider(); // Switch to stun collider
    }

    private void flip()
    {
        float hAxis = Input.GetAxisRaw("Horizontal");

        if ((isFacingRight && hAxis < 0f) || (!isFacingRight && hAxis > 0f))
        {
            isFacingRight = !isFacingRight;
            Vector3 localScale = transform.localScale;
            localScale.x *= -1f;
            transform.localScale = localScale;
        }
    }
}
