using UnityEngine;
using System.Collections;

public class BananaPeel : MonoBehaviour
{
    public float respawnDelay = 7f;

    private Vector3 originalPosition;
    private Quaternion originalRotation;

    private SpriteRenderer spriteRenderer;
    private Collider2D col;

    private bool isRespawning = false;  // Prevent multiple respawns

    void Start()
    {
        originalPosition = transform.position;
        originalRotation = transform.rotation;

        spriteRenderer = GetComponent<SpriteRenderer>();
        col = GetComponent<Collider2D>();
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player") && !isRespawning)
        {
            Debug.Log("Player collided with banana!");

            PlayerMovement player = collision.gameObject.GetComponent<PlayerMovement>();
            if (player != null)
            {
                player.Stun(2f);
            }

            // Prevent further collisions while respawning
            isRespawning = true;

            // Hide and disable to avoid multiple collisions
            spriteRenderer.enabled = false;
            col.enabled = false;

            // Start respawn coroutine
            StartCoroutine(RespawnBanana());
        }
    }

    IEnumerator RespawnBanana()
    {
        Debug.Log($"Banana will respawn in {respawnDelay} seconds.");

        yield return new WaitForSeconds(respawnDelay);

        // Reset to original position and rotation
        transform.position = originalPosition;
        transform.rotation = originalRotation;

        // Enable renderer and collider to make banana visible and solid again
        spriteRenderer.enabled = true;
        col.enabled = true;

        Debug.Log("Banana respawned at original position");

        isRespawning = false;  // Allow collision detection again
    }
}
