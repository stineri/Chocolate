using UnityEngine;
using System.Collections;

public class VisionCone : MonoBehaviour
{
    public float damage = 10f;
    public float damageRate = 1.0f;

    private bool isTouchingPlayer = false;
    private GameObject Player;

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            isTouchingPlayer = true;
            Player = other.gameObject;
            InvokeRepeating(nameof(DamagePlayer), 0f, damageRate);
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            isTouchingPlayer = false;
            CancelInvoke(nameof(DamagePlayer));
        }
    }

    void DamagePlayer()
    {
        if (isTouchingPlayer && Player != null)
        {
            PlayerHide hideScript = Player.GetComponent<PlayerHide>();
            if (hideScript != null && !hideScript.isHiding)
            {
                HealthManager healthManager = Player.GetComponent<HealthManager>();
                Animator animator = Player.GetComponent<Animator>();

                if (healthManager != null)
                {
                    healthManager.TakeDamage(damage);
                }

                if (animator != null)
                {
                    animator.SetTrigger("isDamaged"); // Use Trigger instead of bool
                }
            }
            else
            {
                Debug.Log("Player is hiding – no damage applied.");
            }
        }
    }
}
