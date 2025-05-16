using UnityEngine;

public class PlayerHide : MonoBehaviour
{
    public bool isHiding = false;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("HidingSpot"))
        {
            isHiding = true;
            GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 0.4f); // Semi-transparent
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("HidingSpot"))
        {
            isHiding = false;
            GetComponent<SpriteRenderer>().color = Color.white; // Fully visible again
        }
    }
}
