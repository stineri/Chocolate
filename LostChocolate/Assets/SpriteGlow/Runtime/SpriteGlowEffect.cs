using UnityEngine;

public class SpriteGlowEffect : MonoBehaviour
{
    public float bloomSpeed = 2f;     // Speed of the bloom pulse
    public float maxScale = 1.2f;     // Maximum scale during bloom
    public Color bloomColor = Color.yellow; // Color to pulse to
    private Vector3 originalScale;
    private SpriteRenderer spriteRenderer;
    private Color originalColor;

    void Start()
    {
        originalScale = transform.localScale;
        spriteRenderer = GetComponent<SpriteRenderer>();
        originalColor = spriteRenderer.color;
    }

    void Update()
    {
        float scale = 1 + Mathf.Sin(Time.time * bloomSpeed) * (maxScale - 1);
        transform.localScale = originalScale * scale;

        // Pulse only the alpha channel to make glow appear and disappear smoothly
        float alpha = (Mathf.Sin(Time.time * bloomSpeed) + 1) / 2; // ranges 0 to 1

        Color newColor = originalColor;
        newColor.a = Mathf.Lerp(0.5f, 1f, alpha); // alpha pulses between 0.5 and 1
        spriteRenderer.color = newColor;
    }

}
