using UnityEngine;

public class NoiseTrigger : MonoBehaviour
{
    public float noiseRadius = 5f;

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player")) // Choco
        {
            // Detect all enemies within noise radius
            Collider2D[] nearbyEnemies = Physics2D.OverlapCircleAll(transform.position, noiseRadius);
            foreach (var enemy in nearbyEnemies)
            {
                if (enemy.CompareTag("Enemy"))
                {
                    enemy.GetComponent<EnemyAI>()?.InvestigateSound(transform.position);
                }
            }
        }
    }

    void OnDrawGizmosSelected()
    {
        // Just for visualizing noise radius in Unity Editor
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, noiseRadius);
    }
}
