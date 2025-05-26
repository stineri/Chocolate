using UnityEngine;

public class EnemyNoiseDetector : MonoBehaviour
{
    public string enemyTag = "Enemy";

    void Update()
    {
        bool controlHeld = Input.GetKey(KeyCode.LeftControl) || Input.GetKey(KeyCode.RightControl);

        if (!controlHeld && (Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.LeftShift)))
        {
            GameObject[] enemies = GameObject.FindGameObjectsWithTag(enemyTag);
            if (enemies.Length == 0) return;

            GameObject nearestEnemy = null;
            float minDist = Mathf.Infinity;
            Vector3 playerPos = transform.position;

            foreach (GameObject enemy in enemies)
            {
                enemyPatrol patrolScript = enemy.GetComponent<enemyPatrol>();
                if (patrolScript != null)
                {
                    float dist = patrolScript.DistanceToPlayer(playerPos);
                    if (dist < minDist)
                    {
                        minDist = dist;
                        nearestEnemy = enemy;
                    }
                }
            }

            if (nearestEnemy != null)
            {
                enemyPatrol patrolScript = nearestEnemy.GetComponent<enemyPatrol>();
                patrolScript.GoToCan(playerPos);
            }
        }
    }
}
