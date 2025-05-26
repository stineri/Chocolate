using UnityEngine;

public class PlayerAutoMove : MonoBehaviour
{
    public float autoWalkSpeed = 3f;
    public float jumpForce = 5f;
    public Transform nextLevelTarget; // Assign PlayerNextLevel in Inspector
    private Rigidbody2D rb;
    private bool isAutoMoving = false;
    private bool hasJumped = false;
    private bool hasReachedTarget = false;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        // Trigger auto move when player reaches or passes x = 92
        if (!isAutoMoving && transform.position.x >= 92f)
        {
            isAutoMoving = true;
        }

        if (isAutoMoving && !hasReachedTarget)
        {
            AutoMoveToNextLevel();
        }
    }

    void AutoMoveToNextLevel()
    {
        if (nextLevelTarget == null) return;

        // Direction to target
        Vector2 direction = (nextLevelTarget.position - transform.position).normalized;

        // Walk horizontally
        rb.linearVelocity = new Vector2(autoWalkSpeed * Mathf.Sign(direction.x), rb.linearVelocity.y);

        // Jump if below target and not yet jumped
        if (!hasJumped && transform.position.y < nextLevelTarget.position.y - 0.1f)
        {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpForce);
            hasJumped = true;
        }

        // Stop moving when close enough
        if (Vector2.Distance(transform.position, nextLevelTarget.position) < 0.3f)
        {
            rb.linearVelocity = Vector2.zero;
            hasReachedTarget = true;
        }
    }
}
