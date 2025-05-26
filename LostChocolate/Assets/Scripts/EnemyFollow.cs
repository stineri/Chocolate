using UnityEngine;

public class EnemyFollow : MonoBehaviour
{
    public Transform player;
    private Animator anim;
    private Rigidbody2D rb;

    public float speed = 3f;
    public float stopX = 99f;             // Stop moving at x = 90
    public float startFollowX = 12.5f;    // Start following only after player passes this x
    public float damageRange = 6f;        // Increased range
    public float damageAmount = 10f;
    public float damageCooldown = 1f;
    private float damageTimer = 0f;

    private bool isPaused = false;
    private bool hasStartedFollowing = false;
    private bool facingRight = true;

    private void Start()
    {
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();

        anim.SetBool("isRunning", false);
        anim.SetBool("isIdle", true);
    }

    void Update()
    {
        if (isPaused)
        {
            rb.linearVelocity = Vector2.zero;
            SetAnimation(false);
            return;
        }

        if (player != null)
        {
            // Only start following after player crosses startFollowX
            if (!hasStartedFollowing && player.position.x >= startFollowX)
            {
                hasStartedFollowing = true;
            }

            if (hasStartedFollowing && transform.position.x < stopX)
            {
                float direction = Mathf.Sign(player.position.x - transform.position.x);

                // Move
                Vector3 newPosition = transform.position;
                newPosition.x += speed * Time.deltaTime * direction;
                newPosition.y = -1.73f;
                transform.position = newPosition;

                // Flip based on player position
                if ((direction > 0 && !facingRight) || (direction < 0 && facingRight))
                {
                    Flip();
                }

                SetAnimation(true); // running
            }
            else
            {
                SetAnimation(false); // idle
            }

            // Damage
            damageTimer -= Time.deltaTime;
            if (Vector2.Distance(transform.position, player.position) <= damageRange && damageTimer <= 0f)
            {
                HealthManager playerHealth = player.GetComponent<HealthManager>();
                if (playerHealth != null)
                {
                    playerHealth.TakeDamage(damageAmount);
                    damageTimer = damageCooldown;
                }
            }
        }
    }

    private void SetAnimation(bool isRunning)
    {
        anim.SetBool("isRunning", isRunning);
        anim.SetBool("isIdle", !isRunning);
    }

    private void Flip()
    {
        facingRight = !facingRight;
        Vector3 scale = transform.localScale;
        scale.x *= -1;
        transform.localScale = scale;
    }
}
