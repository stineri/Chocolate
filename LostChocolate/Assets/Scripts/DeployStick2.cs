using System.Collections;
using UnityEngine;

public class DeployStick2 : MonoBehaviour
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

        // Random Y position between 1 and 5
        float randomY = Random.Range(1f, 5f);
        Vector2 fixedPosition = new Vector2(16.4f, randomY); // Fixed X, random Y
        a.transform.position = fixedPosition;

        // Apply physics force to the stick
        Rigidbody2D rb = a.GetComponent<Rigidbody2D>();

        if (rb != null)
        {
            // Apply a horizontal and random vertical force
            Vector2 throwDirection = new Vector2(10f, Random.Range(-5f, 5f));
            rb.AddForce(throwDirection, ForceMode2D.Impulse);
        }

        Debug.Log("Stick spawned at position: " + fixedPosition);
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
