using System.Collections;
using UnityEngine;

public class deployStick : MonoBehaviour
{
    public GameObject stickPrefab;

    // Define the range for random respawn time
    public float minRespawnTime = 3f; // Minimum respawn time (seconds)
    public float maxRespawnTime = 7f; // Maximum respawn time (seconds)

    void Start()
    {
        StartCoroutine(StickWave());
    }

    private void SpawnStick()
    {
        // Instantiate the stick prefab
        GameObject a = Instantiate(stickPrefab);

        // Random X position between 8.51 and 66.5
        float randomX = Random.Range(8.51f, 66.5f);

        // Random Y position between 1 and 5 (keep as is)
        float randomY = Random.Range(1f, 5f);

        // Set the spawn position
        Vector2 spawnPosition = new Vector2(randomX, randomY);
        a.transform.position = spawnPosition;

        // Apply physics force to the stick
        Rigidbody2D rb = a.GetComponent<Rigidbody2D>();
        if (rb != null)
        {
            // Apply a horizontal and random vertical force
            Vector2 throwDirection = new Vector2(7f, Random.Range(-5f, 5f));
            rb.AddForce(throwDirection, ForceMode2D.Impulse);
        }

        Debug.Log("Stick spawned at position: " + spawnPosition);
    }


    // Coroutine to spawn sticks with a random respawn time
    IEnumerator StickWave()
    {
        while (true)
        {
            // Randomize the respawn time between minRespawnTime and maxRespawnTime
            float randomRespawnTime = Random.Range(minRespawnTime, maxRespawnTime);

            // Wait for the randomly determined respawn time before spawning the next stick
            yield return new WaitForSeconds(randomRespawnTime);

            Debug.Log("Spawning stick...");
            SpawnStick(); // Spawn the stick
        }
    }
}
