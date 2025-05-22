using UnityEngine;

public class HealCollectible : MonoBehaviour
{
    public float healingAmount = 20f;

    void OnTriggerEnter2D(Collider2D other)
    {
        // Check if the object has a HealthManager
        HealthManager healthManager = other.GetComponent<HealthManager>();

        if (healthManager != null)
        {
            // Heal the player
            healthManager.Heal(healingAmount);

            // Destroy the collectible
            Destroy(gameObject);
        }
    }
}
