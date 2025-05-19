using UnityEngine;

public class CanTrigger : MonoBehaviour
{
    public enemyPatrol enemy; // Drag the enemy with the script into the Inspector

    private void OnTriggerEnter2D(Collider2D other)
    {
        // Optional: filter who can trigger this
        if (other.CompareTag("Player"))
        {
            enemy.GoToCan(transform.position);
        }
    }
}
