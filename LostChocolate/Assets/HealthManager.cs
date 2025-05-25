using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class HealthManager : MonoBehaviour
{
    public GameObject FloatingTextPrefab;
    public Image healthBar;
    public float healthAmount = 100f;

    public Animator animator;                // Assign your Animator here in Inspector
    public float damageAnimDuration = 5f; // Duration of damage animation
    public float invincibilityDuration = 2f; // Player invincible for 2 seconds

    private bool isInvincible = false;

    void Start()
    {
        healthAmount = Mathf.Clamp(healthAmount, 0, 100);
        UpdateHealthBar();
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Stick") && !isInvincible)
        {
            TakeDamage(10);
            Destroy(other.gameObject);
            PlayDamageAnimation();
        }
    }

    public void TakeDamage(float damage)
    {
        if (isInvincible) return; // Safety check

        healthAmount -= damage;
        healthAmount = Mathf.Clamp(healthAmount, 0, 100);
        Debug.Log("Health after damage: " + healthAmount);
        UpdateHealthBar();

        if (FloatingTextPrefab && healthAmount > 0)
        {
            ShowFloatingText(damage);
        }

        if (animator != null)
        {
            StartCoroutine(PlayDamageAnimation());
        }

        StartCoroutine(InvincibilityFrames());
    }

    private IEnumerator PlayDamageAnimation()
    {
        animator.SetBool("isDamaged", true);
        yield return new WaitForSeconds(damageAnimDuration);
        animator.SetBool("isDamaged", false);
    }

    private IEnumerator InvincibilityFrames()
    {
        isInvincible = true;
        yield return new WaitForSeconds(invincibilityDuration);
        isInvincible = false;
    }

    void ShowFloatingText(float damage)
    {
        var go = Instantiate(FloatingTextPrefab, transform.position, Quaternion.identity);
        var textMesh = go.GetComponent<TextMesh>();
        textMesh.text = damage.ToString();
        textMesh.color = new Color(1f, 0f, 0f, 1f);
        textMesh.fontSize = 18;

        var floatingText = go.GetComponent<FloatingText>();
        floatingText.Target = transform;
    }

    public void Heal(float healingAmount)
    {
        healthAmount += healingAmount;
        healthAmount = Mathf.Clamp(healthAmount, 0, 100);
        UpdateHealthBar();
    }

    private void UpdateHealthBar()
    {
        if (healthBar != null)
        {
            healthBar.fillAmount = healthAmount / 100f;
        }
    }
}
