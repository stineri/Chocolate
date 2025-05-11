using UnityEngine;
using UnityEngine.UI;

public class HealthManager : MonoBehaviour
{
    public GameObject FloatingTextPrefab;
    public Image healthBar;        // Reference to the health bar UI Image
    public float healthAmount = 100f; // Starting health

    // Start is called once before the first execution of Update
    void Start()
    {
        // Ensure health is within a valid range (just in case)
        healthAmount = Mathf.Clamp(healthAmount, 0, 100);
        UpdateHealthBar();
    }

    // Update is called once per frame
    void Update()
    {
        // Your continuous update logic goes here, if needed
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        // When the player is hit by the stick
        if (other.CompareTag("Stick"))
        {
            // Apply damage
            TakeDamage(10);
            // Destroy the stick
            Destroy(other.gameObject);
        }
    }

    public void TakeDamage(float damage)
    {
        healthAmount -= damage;
        healthAmount = Mathf.Clamp(healthAmount, 0, 100);
        Debug.Log("Health after damage: " + healthAmount); // Debug log for health value
        UpdateHealthBar();

        if (FloatingTextPrefab && healthAmount != 0)
        {

            ShowFloatingText(damage);
        }

           
    }

    void ShowFloatingText(float damage)
    {
        var go = Instantiate(FloatingTextPrefab, transform.position, Quaternion.identity, transform);
        var textMesh = go.GetComponent<TextMesh>();
        textMesh.text = damage.ToString();
        textMesh.color = new Color(1f, 0f, 0f, 1f); // Set text color to red for damage indication
    }

    public void Heal(float healingAmount)
    {
        healthAmount += healingAmount;

        // Ensure health doesn't go above 100
        healthAmount = Mathf.Clamp(healthAmount, 0, 100);

        // Update the health bar
        UpdateHealthBar();
    }

    // Helper function to update the health bar fill
    private void UpdateHealthBar()
    {
        if (healthBar != null)
        {
            // Update the fill amount based on health percentage
            healthBar.fillAmount = healthAmount / 100f;
        }
    }
}
