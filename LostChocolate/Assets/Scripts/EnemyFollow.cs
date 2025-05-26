using UnityEngine;

public class EnemyFollow : MonoBehaviour
{
    public Transform player;
    public float speed = 3f;
    public float stopX = 81f;
    public float startFollowX = 12.5f;
    public float damageRange = 3f;
    public float damageAmount = 10f;
    public float damageCooldown = 1f;

    private float damageTimer = 0f;

    void Update()
    {
        // Follow player only if player has passed x = 12.5
        if (player != null && player.position.x >= startFollowX && transform.position.x < stopX)
        {
            Vector3 direction = (player.position - transform.position).normalized;
            Vector3 move = new Vector3(direction.x, 0f, 0f);
            transform.position += move * speed * Time.deltaTime;
        }

        // Damage player if in range
        damageTimer -= Time.deltaTime;

        if (player != null && Vector2.Distance(transform.position, player.position) <= damageRange && damageTimer <= 0f)
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
