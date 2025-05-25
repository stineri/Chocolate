using UnityEngine;

public class PlayerHide : MonoBehaviour
{
    public bool isHiding = false;
    private bool canHide = false;
    private SpriteRenderer spriteRenderer;

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        if (canHide && Input.GetKeyDown(KeyCode.E))
        {
            isHiding = !isHiding; // Toggle hiding

            if (isHiding)
            {
                spriteRenderer.color = new Color(1, 1, 1, 0.4f); // Semi-transparent
            }
            else
            {
                spriteRenderer.color = Color.white; // Fully visible
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("HidingSpot"))
        {
            canHide = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("HidingSpot"))
        {
            canHide = false;
            isHiding = false;
            spriteRenderer.color = Color.white; // Ensure visible on exit
        }
    }
}
