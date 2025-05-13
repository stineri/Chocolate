using UnityEngine;
using System.Collections;

public class BananaPeel : MonoBehaviour
{
    public float respawnDelay = 5f;

    private Vector3 originalPosition;
    private Quaternion originalRotation;

    void Start()
    {
        originalPosition = transform.position;
        originalRotation = transform.rotation;
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Debug.Log("Player collided with banana!");

            PlayerMovement player = collision.gameObject.GetComponent<PlayerMovement>();
            if (player != null)
            {
                player.Stun(2f);
            }

            // Start coroutine before disabling
            StartCoroutine(RespawnBanana());

            // Disable banana
            gameObject.SetActive(false);
        }
    }

    IEnumerator RespawnBanana()
    {
        Debug.Log("Respawn coroutine started, waiting for " + respawnDelay + " seconds.");

        yield return new WaitForSeconds(respawnDelay);

        // Reset position/rotation in case it moved
        transform.position = originalPosition;
        transform.rotation = originalRotation;

        Debug.Log("Banana respawned!");

        // Reactivate the banana
        gameObject.SetActive(true);
    }
}
