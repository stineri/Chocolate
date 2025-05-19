using UnityEngine;
using System.Collections;

public class enemyPatrol : MonoBehaviour
{
    public GameObject PointA;
    public GameObject PointB;
    private Rigidbody2D rb;
    private Animator anim;
    private Transform currPoint;
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
        anim.SetBool("isRunning", true);

        originalScale = transform.localScale;
    }

    void Update()
    {
        if (isPaused)
        {
            rb.linearVelocity = Vector2.zero;
            return;
        }

        if (isGoingToCan)
        {
            Vector2 direction = (canPosition - transform.position).normalized;
            rb.linearVelocity = direction * speed;

            SetFacingDirection((direction.x >= 0) ? 1 : -1);

            float dist = Vector2.Distance(transform.position, canPosition);

            Debug.Log("Distance to can: " + dist);

            if (dist <= 1.5f)
            {
                rb.linearVelocity = Vector2.zero;
                isGoingToCan = false;
                Debug.Log("Enemy reached the Can! Starting LookAroundThenResume coroutine.");
                StartCoroutine(LookAroundThenResume());
            }
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
        anim.SetBool("isRunning", false);
        rb.linearVelocity = Vector2.zero;

        yield return new WaitForSeconds(3f);

        int faceDir = (targetPosition.x >= transform.position.x) ? 1 : -1;
        SetFacingDirection(faceDir);

        anim.SetBool("isRunning", true);
        canPosition = targetPosition;
        isPaused = false;
        isGoingToCan = true;
    }

    private IEnumerator LookAroundThenResume()
    {
        isPaused = true;
        rb.linearVelocity = Vector2.zero;
        anim.SetBool("isRunning", false);

        int currentDir = (transform.localScale.x > 0) ? 1 : -1;

        // Look to other side
        SetFacingDirection(-currentDir);
        yield return new WaitForSeconds(3f);

        // Look back
        SetFacingDirection(currentDir);
        yield return new WaitForSeconds(3f);

        anim.SetBool("isRunning", true);
        isPaused = false;

        // Set the next patrol point
        float distA = Vector2.Distance(transform.position, PointA.transform.position);
        float distB = Vector2.Distance(transform.position, PointB.transform.position);
        currPoint = (distA < distB) ? PointA.transform : PointB.transform;

        Debug.Log("Enemy resumes patrolling towards: " + currPoint.name);
    }
}
