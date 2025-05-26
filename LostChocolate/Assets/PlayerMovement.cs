using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class PlayerMovement : MonoBehaviour
{
    public float speed = 5f;
    public float runSpeed = 10f;
    public float crouchSpeed = 2f;
    public float jumpForce = 10f;
    public float Stamina, MaxStamina, RunCost, ChargeRate;

    public Image StaminaBar;

    private Rigidbody2D rb;
    private bool grounded = false;
    private bool isStunned = false;
    private float stunTimer = 0f;
    private bool isFacingRight = true;
    private Coroutine recharge;

    public Collider2D normalCollider;
    public Collider2D stunCollider;
    public Collider2D crouchCollider;

    [SerializeField] private Animator animator;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        EnableNormalCollider();
    }

    void Update()
    {
        if (isStunned)
        {
            HandleStun();
            return;
        }

        float hAxis = Input.GetAxisRaw("Horizontal");
        bool isMoving = hAxis != 0f;
        bool isCrouching = Input.GetKey(KeyCode.LeftControl);

        if (isCrouching)
        {
            EnableCrouchCollider();
        }
        else
        {
            EnableNormalCollider();
        }

        Flip(hAxis);

        if (Input.GetKey(KeyCode.LeftShift) && isMoving && !isCrouching && Stamina > 0f)
        {
            rb.linearVelocity = new Vector2(hAxis * runSpeed, rb.linearVelocity.y);
            Stamina -= RunCost *Time.deltaTime;
            if(Stamina < 0f) Stamina = 0f;
            StaminaBar.fillAmount = Stamina / MaxStamina;

            if(recharge != null) StopCoroutine(recharge);
            recharge = StartCoroutine(RechargeStamina());
            SetAnimState("isRunning");
        }
        else if (isCrouching && isMoving)
        {
            rb.linearVelocity = new Vector2(hAxis * crouchSpeed, rb.linearVelocity.y);
            SetAnimState("isCrouching");
        }
        else if (isMoving)
        {
            rb.linearVelocity = new Vector2(hAxis * speed, rb.linearVelocity.y);
            SetAnimState("IsWalking");
        }
        else
        {
            rb.linearVelocity = new Vector2(0f, rb.linearVelocity.y);
            SetAnimState("isIdle");
        }

        if (Input.GetKeyDown(KeyCode.Space) && grounded)
        {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpForce);
            grounded = false;
            animator.SetBool("IsJumping", true);
        }

        if (grounded)
        {
            animator.SetBool("IsJumping", false);
        }


    }

    private IEnumerator RechargeStamina ()
    {
        yield return new WaitForSeconds(3f);

        while (Stamina < MaxStamina)
        {
            Stamina += ChargeRate / 10f;
            if (Stamina > MaxStamina) Stamina = MaxStamina;
            StaminaBar.fillAmount = Stamina / MaxStamina;
            yield return new WaitForSeconds (.1f);
        }
    }

    private void HandleStun()
    {
        stunTimer -= Time.deltaTime;
        rb.linearVelocity = new Vector2(Mathf.Lerp(rb.linearVelocity.x, 0f, Time.deltaTime * 10f), rb.linearVelocity.y);

        SetAnimState("isStunned");

        if (stunTimer <= 0f)
        {
            isStunned = false;
            animator.SetBool("isStunned", false);
            EnableNormalCollider();
        }
    }



    public void EnableNormalCollider()
    {
        if (normalCollider && crouchCollider)
        {
            crouchCollider.enabled = false;
            normalCollider.enabled = true;
            Debug.Log("Normal collider enabled");
        }
    }

    public void EnableCrouchCollider()
    {
        if (normalCollider && crouchCollider)
        {
            normalCollider.enabled = false;
            crouchCollider.enabled = true;
            Debug.Log("Crouch collider enabled");
        }
    }

    private void SetAnimState(string state)
    {
        string[] states = { "IsWalking", "isRunning", "isIdle", "IsJumping", "isCrouching", "isStunned" };
        foreach (string s in states) animator.SetBool(s, false);
        animator.SetBool(state, true);
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Ground"))
        {
            grounded = true;
        }


    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("StunObject"))
        {
            Stun(2f); // Stun for 2 seconds
        }
    }
    private void OnCollisionExit2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Ground"))
        {
            grounded = false;
        }
    }

    public void Stun(float duration)
    {
        if (isStunned) return;

        isStunned = true;
        stunTimer = duration;
        rb.linearVelocity = Vector2.zero;
    }

    private void Flip(float horizontal)
    {
        if ((isFacingRight && horizontal < 0f) || (!isFacingRight && horizontal > 0f))
        {
            isFacingRight = !isFacingRight;
            Vector3 localScale = transform.localScale;
            localScale.x *= -1f;
            transform.localScale = localScale;
        }
    }
}