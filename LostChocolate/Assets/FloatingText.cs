using UnityEngine;

public class FloatingText : MonoBehaviour
{
    public float DestroyTime = 1f;
    public Vector3 Offset = new Vector3(0, 2, 0);
    public float TextScale = 2f;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Destroy(gameObject, DestroyTime);

        transform.localPosition += Offset;

        transform.localScale *= TextScale;

        var textMesh = GetComponent<TextMesh>();
        textMesh.color = Color.red;
    }

    // Update is called once per frame
    
}
