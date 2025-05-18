using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float speed = 5f;
    public float runSpeed = 10f;
    public float crouchSpeed = 2f;
    public float jumpForce = 10f; // Adjusted to fit typical Rigidbody2D scale

    private Rigidbody2D rb;
    private bool grounded = false;

    private bool isStunned = false;
    private float stunTimer = 0f;
    private bool isFacingRIght = true;

    [SerializeField] private Animator animator;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        // Handle stun logic
        if (isStunned)
        {
            stunTimer -= Time.deltaTime;
            rb.linearVelocity = new Vector2(0f, rb.linearVelocity.y); // Stop horizontal movement but let vertical motion (falling) continue

            if (stunTimer <= 0f)
            {
                isStunned = false;
            }

            return; // Skip movement and jumping input while stunned
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
            grounded = false; // Assume we're jumping
        }

        if (hAxis != 0 )
        {
            animator.SetBool("isRunning", true);
        } else
        {
            animator.SetBool("isRunning", false);

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

    public void Stun(float duration)
    {
        Debug.Log("Player got stunned!");
        isStunned = true;
        stunTimer = duration;
        rb.linearVelocity = Vector2.zero; // Stop immediately
    }

    private void flip()
    {
       float hAxis = Input.GetAxisRaw("Horizontal");

        if ((isFacingRIght && hAxis < 0f) || (!isFacingRIght && hAxis > 0f))
        {
            isFacingRIght = !isFacingRIght;
            Vector3 localScale = transform.localScale;
            localScale.x *= -1f;
            transform.localScale = localScale;
        }
    }
}
