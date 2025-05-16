using UnityEngine;
using System.Collections;

public class EnemyAI : MonoBehaviour
{
    public GameObject PointA;
    public GameObject PointB;
    public float speed = 2f;
    public float investigateDuration = 3f;
    public GameObject exclamationMark;

    private Rigidbody2D rb;
    private Animator anim;
    private Transform currPoint;
    private Vector3 startPosition;

    private bool isInvestigating = false;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();

        currPoint = PointB.transform;
        startPosition = transform.position;

        if (exclamationMark != null)
            exclamationMark.SetActive(false);

        anim.SetBool("isRunning", true);
    }

    void Update()
    {
        if (isInvestigating)
        {
            rb.linearVelocity = Vector2.zero;  // Stop moving during investigation
            return;
        }

        Patrol();
    }

    void Patrol()
    {
        Vector2 targetPos = currPoint.position;
        Vector2 direction = (targetPos - (Vector2)transform.position).normalized;

        rb.linearVelocity = new Vector2(direction.x * speed, rb.linearVelocity.y);

        // Flip sprite depending on direction
        if (direction.x > 0)
            transform.localScale = new Vector3(Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);
        else if (direction.x < 0)
            transform.localScale = new Vector3(-Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);

        // Check distance to current point
        if (Vector2.Distance(transform.position, targetPos) < 0.1f)
        {
            currPoint = (currPoint == PointB.transform) ? PointA.transform : PointB.transform;
        }
    }

    public void InvestigateSound(Vector3 soundPosition)
    {
        if (!isInvestigating)
        {
            StartCoroutine(InvestigateRoutine(soundPosition));
        }
    }

    IEnumerator InvestigateRoutine(Vector3 target)
    {
        isInvestigating = true;

        if (exclamationMark != null)
            exclamationMark.SetActive(true);

        // Idle first before moving
        anim.SetBool("isRunning", false);
        anim.SetBool("isIdle", true);
        rb.linearVelocity = Vector2.zero;
        yield return new WaitForSeconds(1f);

        // Move towards the sound
        anim.SetBool("isIdle", false);
        anim.SetBool("isRunning", true);

        while (Vector2.Distance(transform.position, target) > 0.05f)
        {
            Vector2 moveDir = (target - transform.position).normalized;
            rb.linearVelocity = new Vector2(moveDir.x * speed, rb.linearVelocity.y);

            // Flip to face movement direction
            if (moveDir.x > 0)
                transform.localScale = new Vector3(Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);
            else if (moveDir.x < 0)
                transform.localScale = new Vector3(-Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);

            yield return null;
        }

        rb.linearVelocity = Vector2.zero;

        // Stop moving and idle
        anim.SetBool("isRunning", false);
        anim.SetBool("isIdle", true);

        // Face the sound direction explicitly
        float directionToSound = target.x - transform.position.x;
        if (Mathf.Abs(directionToSound) > 0.1f)
        {
            Vector3 scale = transform.localScale;
            scale.x = Mathf.Sign(directionToSound) * Mathf.Abs(scale.x);
            transform.localScale = scale;
        }

        // Look left and right for investigateDuration seconds
        float timer = 0f;
        float flipInterval = 1f;

        while (timer < investigateDuration)
        {
            timer += Time.deltaTime;

            if (Mathf.FloorToInt(timer / flipInterval) % 2 == 1)
            {
                Vector3 scale = transform.localScale;
                scale.x *= -1;
                transform.localScale = scale;
            }

            yield return null;
        }

        // Return to patrol start position
        anim.SetBool("isIdle", false);
        anim.SetBool("isRunning", true);

        while (Vector2.Distance(transform.position, startPosition) > 0.05f)
        {
            Vector2 moveDir = (startPosition - transform.position).normalized;
            rb.linearVelocity = new Vector2(moveDir.x * speed, rb.linearVelocity.y);

            // Flip to face movement direction
            if (moveDir.x > 0)
                transform.localScale = new Vector3(Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);
            else if (moveDir.x < 0)
                transform.localScale = new Vector3(-Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);

            yield return null;
        }

        rb.linearVelocity = Vector2.zero;

        anim.SetBool("isRunning", true);

        if (exclamationMark != null)
            exclamationMark.SetActive(false);

        isInvestigating = false;
    }
}
