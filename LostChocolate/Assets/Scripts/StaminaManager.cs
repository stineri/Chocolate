using UnityEngine;
using UnityEngine.UI; // For UI display (optional)

public class StaminaManager : MonoBehaviour
{
    public float maxStamina = 100f;
    public float currentStamina;
    public float staminaDrainRate = 20f;   // per second
    public float staminaRegenRate = 10f;   // per second
    public float regenDelay = 2f;

    private float regenTimer;

    public Slider staminaBar; // Optional: for UI

    void Start()
    {
        currentStamina = maxStamina;

        if (staminaBar != null)
            staminaBar.maxValue = maxStamina;
    }

    void Update()
    {
        if (regenTimer > 0)
            regenTimer -= Time.deltaTime;
        else
            RegenerateStamina();

        if (staminaBar != null)
            staminaBar.value = currentStamina;
    }

    public bool UseStamina(float amount)
    {
        if (currentStamina >= amount)
        {
            currentStamina -= amount;
            regenTimer = regenDelay;
            return true;
        }
        return false;
    }

    void RegenerateStamina()
    {
        if (currentStamina < maxStamina)
        {
            currentStamina += staminaRegenRate * Time.deltaTime;
            currentStamina = Mathf.Min(currentStamina, maxStamina);
        }
    }
}
