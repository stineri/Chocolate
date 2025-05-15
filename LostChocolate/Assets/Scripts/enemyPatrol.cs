using UnityEngine;

public class enemyPatrol : MonoBehaviour
{
    public GameObject PointA;
    public GameObject PointB;
    private Rigidbody2D rb;
    private Animator anim;
    private Transform currPoint;
    public float speed;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        currPoint = PointB.transform;
        anim.SetBool("isRunning", true);

    }

    // Update is called once per frame
    void Update()
    {
        Vector2 point = currPoint.position - transform.position;
        if (currPoint == PointB.transform)
        {
            rb.linearVelocity = new Vector2(speed, 0);
        }
        else
        {
            rb.linearVelocity = new Vector2(-speed, 0);
        }

        if (Vector2.Distance(transform.position, currPoint.position) < 0.5f && currPoint == PointB.transform)
        {
            flip();
            currPoint = PointA.transform;
        }

        if (Vector2.Distance(transform.position, currPoint.position) < 0.5f && currPoint == PointA.transform)
        {
            flip();
            currPoint = PointB.transform;
        }
    }

    private void flip()
    {
        Vector3 localScale = transform.localScale;
        localScale.x *= -1;
        transform.localScale = localScale;
    }
}
