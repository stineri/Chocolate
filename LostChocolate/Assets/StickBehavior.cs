using UnityEngine;

public class StickBehavior : MonoBehaviour
{
    private bool isScheduledForDestruction = false;

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (!isScheduledForDestruction && collision.collider.CompareTag("Ground"))
        {
            isScheduledForDestruction = true;
            Destroy(gameObject, 3f); // Destroy the stick after 3 seconds
        }
    }
}
