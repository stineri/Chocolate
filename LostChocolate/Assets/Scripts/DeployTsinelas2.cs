using System.Collections;
using UnityEngine;

public class DeployTsinelas2 : MonoBehaviour
{
    public GameObject tsinelasPrefab;

    // Define the range for random respawn time
    public float minRespawnTime = 5f; // Minimum respawn time (seconds)
    public float maxRespawnTime = 10f; // Maximum respawn time (seconds)

    void Start()
    {
        StartCoroutine(TsinelasWave());
    }

    private void SpawnTsinelas()
    {
        // Instantiate the tsinelas prefab
        GameObject a = Instantiate(tsinelasPrefab);

        // Random X position between 8.51 and 66.5
        float randomX = Random.Range(8.51f, 66.5f);

        // Random Y position between 1 and 5 (unchanged)
        float randomY = Random.Range(1f, 5f);

        // Set the spawn position
        Vector2 spawnPosition = new Vector2(randomX, randomY);
        a.transform.position = spawnPosition;

        // Apply physics force to the tsinelas
        Rigidbody2D rb = a.GetComponent<Rigidbody2D>();
        if (rb != null)
        {
            // Apply a horizontal and random vertical force
            Vector2 throwDirection = new Vector2(7f, Random.Range(-5f, 5f));
            rb.AddForce(throwDirection, ForceMode2D.Impulse);
        }

        Debug.Log("Tsinelas spawned at position: " + spawnPosition);
    }

    // Coroutine to spawn tsinelas with a random respawn time
    IEnumerator TsinelasWave()
    {
        while (true)
        {
            // Randomize the respawn time between minRespawnTime and maxRespawnTime
            float randomRespawnTime = Random.Range(minRespawnTime, maxRespawnTime);

            // Wait for the random respawn time
            yield return new WaitForSeconds(randomRespawnTime);

            Debug.Log("Spawning tsinelas...");
            SpawnTsinelas();
        }
    }
}
