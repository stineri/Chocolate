using UnityEngine;

public class PlatformRepeater : MonoBehaviour
{
    public GameObject platformPrefab; // The prefab to repeat
    public int repeatCount = 10; // Number of segments

    void Start()
    {
        float width = platformPrefab.GetComponent<SpriteRenderer>().bounds.size.x;

        for (int i = -13; i < repeatCount; i++)
        {
            Vector3 pos = new Vector3(i * width, transform.position.y, transform.position.z);
            Instantiate(platformPrefab, pos, Quaternion.identity, transform);
        }
    }
}
