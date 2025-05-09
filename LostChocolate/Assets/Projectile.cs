using UnityEngine;

public class Stick : MonoBehaviour
{
    public float speed = 3f;
    private Rigidbody2D rb;
    private Vector2 boundary;
    public float damage = 10f;
    public float damageRate = 1.0f;
    private bool isTouchingPlayer = false;
    private GameObject Player;

    void Start()
    {
        // Randomize speed between 10 and 18
        speed = Random.Range(7f, 18f);

        rb = GetComponent<Rigidbody2D>();
        rb.linearVelocity = new Vector2(-speed, 0);

        boundary = Camera.main.ScreenToWorldPoint(
            new Vector3(Screen.width, Screen.height, Camera.main.transform.position.z)
        );
    }

    void Update()
    {
        if (transform.position.x < -boundary.x - 2)
        {
            Destroy(gameObject);
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            isTouchingPlayer = true;
            Player = other.gameObject;
            InvokeRepeating(nameof(DamagePlayer), 0f, damageRate);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            isTouchingPlayer = false;
            CancelInvoke(nameof(DamagePlayer));
        }
    }

    void DamagePlayer()
    {
        if (isTouchingPlayer && Player != null)
        {
            Player.GetComponent<HealthManager>()?.TakeDamage(damage);
        }
    }
}
