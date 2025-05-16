using UnityEngine;

public class NoiseTrigger : MonoBehaviour
{
    [Tooltip("Optional: Assign the enemy manually to avoid FindWithTag.")]
    public EnemyAI enemyAI;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            if (enemyAI == null)
            {
                GameObject enemy = GameObject.FindWithTag("Enemy"); // fallback
                if (enemy != null)
                {
                    enemyAI = enemy.GetComponent<EnemyAI>();
                }
            }

            if (enemyAI != null)
            {
                enemyAI.InvestigateSound(transform.position);
            }
        }
    }
}
