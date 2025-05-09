using UnityEngine;

public class StickBehavior : MonoBehaviour
{
    private bool isScheduledForDestruction = false;

    void Update()
    {
        // Rotate around Z-axis
        transform.Rotate(0f, 0f, 500f * Time.deltaTime); // Adjust speed as needed
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (!isScheduledForDestruction && collision.collider.CompareTag("Ground"))
        {
            isScheduledForDestruction = true;
            Destroy(gameObject, 3f); // Destroy the stick after 3 seconds
        }
    }
}
