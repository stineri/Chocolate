using UnityEngine;

public class EnemyFollow : MonoBehaviour
{
    public Transform player;
    public float speed = 3f;
    public float stopX = 81f;
    public float damageRange = 1.5f;
    public float damageAmount = 10f;
    public float damageCooldown = 1f;

    private float damageTimer = 0f;

    void Update()
    {
        // Follow player until x = 81
        if (transform.position.x < stopX && player != null)
        {
            Vector3 direction = (player.position - transform.position).normalized;
            Vector3 move = new Vector3(direction.x, 0f, 0f);
            transform.position += move * speed * Time.deltaTime;
        }

        // Damage player if in range
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