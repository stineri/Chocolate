using UnityEngine;
using System.Collections;

public class enemyPatrol : MonoBehaviour
{
    public GameObject PointA;
    public GameObject PointB;
    private Rigidbody2D rb;
    private Animator anim;
    private Transform currPoint;
    private Transform lastCurrPoint; // Stores previous patrol direction
    public float speed;

    private bool isPaused = false;
    private bool isGoingToCan = false;
    private Vector3 canPosition;
    private Vector3 originalScale;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        currPoint = PointB.transform;
        originalScale = transform.localScale;

        // Start moving
        anim.SetBool("isRunning", true);
        anim.SetBool("isIdle", false);
    }

    void Update()
    {
        if (isPaused)
        {
            rb.linearVelocity = Vector2.zero;
            anim.SetBool("isRunning", false);
            anim.SetBool("isIdle", true);
            return;
        }

        if (isGoingToCan)
        {
            Vector2 direction = (canPosition - transform.position).normalized;
            rb.linearVelocity = direction * speed;

            SetFacingDirection((direction.x >= 0) ? 1 : -1);

            float dist = Vector2.Distance(transform.position, canPosition);
            if (dist <= 1.3f)
            {
                rb.linearVelocity = Vector2.zero;
                isGoingToCan = false;
                StartCoroutine(LookAroundThenResume());
            }

            anim.SetBool("isRunning", true);
            anim.SetBool("isIdle", false);
            return;
        }

        // Regular patrol movement
        Vector2 patrolDirection;
        if (currPoint == PointB.transform)
        {
            patrolDirection = Vector2.right;
            rb.linearVelocity = new Vector2(speed, 0);
        }
        else
        {
            patrolDirection = Vector2.left;
            rb.linearVelocity = new Vector2(-speed, 0);
        }

        SetFacingDirection((patrolDirection.x >= 0) ? 1 : -1);

        if (Vector2.Distance(transform.position, currPoint.position) < 0.5f)
        {
            currPoint = (currPoint == PointB.transform) ? PointA.transform : PointB.transform;
        }

        anim.SetBool("isRunning", true);
        anim.SetBool("isIdle", false);
    }

    private void SetFacingDirection(int direction)
    {
        Vector3 scale = transform.localScale;
        scale.x = Mathf.Abs(originalScale.x) * direction;
        transform.localScale = scale;
    }

    public void GoToCan(Vector3 targetPosition)
    {
        StartCoroutine(PauseThenGoToCan(targetPosition));
    }

    private IEnumerator PauseThenGoToCan(Vector3 targetPosition)
    {
        isPaused = true;
        rb.linearVelocity = Vector2.zero;
        anim.SetBool("isRunning", false);
        anim.SetBool("isIdle", true);

        lastCurrPoint = currPoint;

        yield return new WaitForSeconds(3f);

        int faceDir = (targetPosition.x >= transform.position.x) ? 1 : -1;
        SetFacingDirection(faceDir);

        anim.SetBool("isRunning", true);
        anim.SetBool("isIdle", false);

        canPosition = targetPosition;
        isPaused = false;
        isGoingToCan = true;
    }

    private IEnumerator LookAroundThenResume()
    {
        isPaused = true;
        rb.linearVelocity = Vector2.zero;
        anim.SetBool("isRunning", false);
        anim.SetBool("isIdle", true);

        int currentDir = (transform.localScale.x > 0) ? 1 : -1;

        // Look to the other side
        SetFacingDirection(-currentDir);
        yield return new WaitForSeconds(3f);

        // Look back
        SetFacingDirection(currentDir);
        yield return new WaitForSeconds(3f);

        anim.SetBool("isRunning", true);
        anim.SetBool("isIdle", false);

        isPaused = false;
        isGoingToCan = false;

        currPoint = lastCurrPoint;
    }
}
