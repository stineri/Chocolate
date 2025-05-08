using System.Collections;
using UnityEngine;

public class deployStick : MonoBehaviour
{
    public GameObject stickPrefab;
    public float respawnTime = 4.0f;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        StartCoroutine(StickWave());
    }

    private void SpawnStick()
    {
        // Instantiate the stick prefab at the fixed position (8.51, 0.13)
        GameObject a = Instantiate(stickPrefab);
        Vector2 fixedPosition = new Vector2(8.51f, 1f);
        a.transform.position = fixedPosition;

        // Get the Rigidbody2D component to apply physics-based force
        Rigidbody2D rb = a.GetComponent<Rigidbody2D>();

        if (rb != null)
        {
            // Apply a strong horizontal force and random vertical force to simulate a throw
            Vector2 throwDirection = new Vector2(10000000000000000f, Random.Range(-10f, 10f)); // Strong horizontal and random vertical force
            rb.AddForce(throwDirection, ForceMode2D.Impulse); // Apply force immediately
        }

        Debug.Log("Stick position: " + fixedPosition);
    }

    // Coroutine to continuously spawn sticks at intervals
    IEnumerator StickWave()
    {
        while (true)
        {
            yield return new WaitForSeconds(respawnTime); // Wait for the respawn time
            SpawnStick(); // Spawn a new stick
        }
    }
}
