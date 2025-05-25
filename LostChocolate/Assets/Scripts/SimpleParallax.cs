using UnityEngine;

public class SimpleParallax : MonoBehaviour
{
    public Renderer backgroundRenderer;
    public float parallaxSpeed = 0.1f;

    private Transform cam;
    private Vector3 camStartPos;

    private void Start()
    {
        cam = Camera.main.transform;
        camStartPos = cam.position;

        if (backgroundRenderer == null)
        {
            Debug.LogError("Background Renderer not assigned!");
        }
    }

    private void LateUpdate()
    {
        float distance = cam.position.x - camStartPos.x;
        backgroundRenderer.material.SetTextureOffset("_MainTex", new Vector2(distance * parallaxSpeed, 0));
        transform.position = new Vector3(cam.position.x, cam.position.y, 0);
    }
}
