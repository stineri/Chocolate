using UnityEngine;

public class FloatingText : MonoBehaviour
{
    public float DestroyTime = 0.5f;
    public Vector3 Offset = new Vector3(0, 2, 0);
    public float TextScale = 2f;
    public Transform Target; // Reference to the player

    void Start()
    {
        Destroy(gameObject, DestroyTime);
        transform.localScale *= TextScale;

        var textMesh = GetComponent<TextMesh>();
        textMesh.color = Color.red;
    }

    void Update()
    {
        if (Target != null)
        {
            // Update position to follow the target with the specified offset
            transform.position = Target.position + Offset;
        }
    }
}
