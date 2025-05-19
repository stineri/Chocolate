using UnityEngine;

public class SteppedSodaCan : MonoBehaviour
{
    public Sprite newSprite; // assign in Inspector
    private SpriteRenderer spriteRenderer;

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        // You can check if the other collider is the player or something specific
        if (other.CompareTag("Player"))
        {
            spriteRenderer.sprite = newSprite;
        }
    }
}
