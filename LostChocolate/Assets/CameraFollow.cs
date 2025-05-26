using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform player;
    public float smoothSpeed = 0.125f;
    public Vector3 offset;

    // Step 1: Camera bounds
    public float minX = 0f;
    public float maxX = 10f;

    void LateUpdate()
    {
        Vector3 desiredPosition = new Vector3(player.position.x, transform.position.y, transform.position.z) + offset;

        // Step 1: Clamp the x position
        desiredPosition.x = Mathf.Clamp(desiredPosition.x, minX, maxX);

        Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);
        transform.position = smoothedPosition;
    }
}
