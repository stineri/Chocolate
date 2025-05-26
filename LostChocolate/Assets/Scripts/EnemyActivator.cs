using UnityEngine;

public class EnemyActivator : MonoBehaviour
{
    public Transform player;            // Drag Choco/player here
    public GameObject enemy;            // Drag the enemy GameObject here
    public float triggerX = 12.5f;      // X position of Choco to trigger
    public Vector2 spawnPosition = new Vector2(3f, 0f);  // Position where enemy will appear
    private bool hasActivated = false;

    void Update()
    {
        if (!hasActivated && player.position.x >= triggerX)
        {
            enemy.transform.position = spawnPosition;  // Move enemy to (3, y)
            enemy.SetActive(true);                     // Activate enemy
            hasActivated = true;
        }
    }
}
