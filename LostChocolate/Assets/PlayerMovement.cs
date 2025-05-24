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

    public Collider2D normalCollider; // Assign manually in Inspector
    public Collider2D stunCollider;   // Assign manually in Inspector

    [SerializeField] private Animator animator;

    // Optional: smooth deceleration while stunned
    private float smoothStopSpeed = 10f;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        EnableNormalCollider(); // Start with normal collider active
    }

    void Update()
    {
        if (isStunned)
        {
            HandleStun();
            return;
        }

        EnableNormalCollider(); // Ensure normalCollider is used while not stunned

        float hAxis = Input.GetAxis("Horizontal");
        Flip(hAxis);

        // Movement
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

        // Jump
        if (Input.GetKeyDown(KeyCode.Space) && grounded)
        {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpForce);
            grounded = false;
            animator.SetBool("IsJumping", hAxis != 0); // Set jump animation
        }

        // Animation
        animator.SetBool("isRunning", hAxis != 0);
    }

    private void HandleStun()
    {
        stunTimer -= Time.deltaTime;

        // Smoothly stop horizontal movement
        float smoothedX = Mathf.Lerp(rb.linearVelocity.x, 0f, Time.deltaTime * smoothStopSpeed);
        rb.linearVelocity = new Vector2(smoothedX, rb.linearVelocity.y);

        animator.SetBool("isStunned", true);

        if (stunTimer <= 0f)
        {
            isStunned = false;
            animator.SetBool("isStunned", false);
            EnableNormalCollider();
            Debug.Log("Stun ended — switched to normal collider.");
        }
    }

    public void EnableStunCollider()
    {
        if (normalCollider != null && stunCollider != null)
        {
            normalCollider.enabled = false;
            stunCollider.enabled = true;
            Debug.Log("Switched to Stun Collider");
        }
    }

    public void EnableNormalCollider()
    {
        if (normalCollider != null && stunCollider != null)
        {
            stunCollider.enabled = false;
            normalCollider.enabled = true;
            Debug.Log("Switched to Normal Collider");
        }
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Ground"))
        {
            grounded = true;
            animator.SetBool("IsJumping", false); // Reset jump animation
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
        if (isStunned) return;  // Ignore if already stunned

        Debug.Log("Player got stunned!");
        isStunned = true;
        stunTimer = duration;
        rb.linearVelocity = Vector2.zero;
        animator.SetBool("isStunned", true);
        EnableStunCollider(); // Switch to stun collider
    }

    private void Flip(float horizontal)
    {
        if ((isFacingRight && horizontal < 0f) || (!isFacingRight && horizontal > 0f))
        {
            isFacingRight = !isFacingRight;
            Vector3 localScale = transform.localScale;
            localScale.x *= -1f;
            transform.localScale = localScale;
        }
    }
}
