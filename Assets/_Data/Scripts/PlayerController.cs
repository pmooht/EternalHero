using UnityEngine;

public class PlayerController : TBehaviour
{
    [Header("Base Components")]
    [SerializeField] protected Rigidbody2D rb;
    [SerializeField] protected Animator animator;

    [Header("Movement Settings")]
    [SerializeField] protected float speed = 2.5f;
    [SerializeField] protected float jumpForce = 6f;
    [SerializeField] protected Vector2 movement;
    [SerializeField] protected bool facingRight = true;

    [Header("Ground Check")]
    [SerializeField] protected Transform groundCheck;
    [SerializeField] protected float checkRadius = 0.2f;
    [SerializeField] protected LayerMask groundLayer;
    [SerializeField] protected bool isGrounded;

    [Header("Attack Settings")]
    [SerializeField] protected float attackCooldown = 0.5f;   // Thời gian giữa các đòn tấn công
    [SerializeField] protected float nextAttackTime = 0f;      // Thời điểm có thể tấn công tiếp
    [SerializeField] protected Transform attackPoint;          // Vị trí tấn công
    [SerializeField] protected float attackRange = 0.5f;       // Phạm vi tấn công
    [SerializeField] protected LayerMask enemyLayers;          // Layer của kẻ địch

    protected virtual void Update()
    {
        float horizontalInput = 0f;

        if (Input.GetKey(KeyCode.A))
            horizontalInput = -1f;
        else if (Input.GetKey(KeyCode.D))
            horizontalInput = 1f;

        movement = new Vector2(horizontalInput, 0f).normalized;

        FlipCharacter(horizontalInput);
        HandleJumpInput();
        HandleAttackInput();
        UpdateAnimator();
    }

    protected virtual void FixedUpdate()
    {
        CheckGrounded();

        float horizontalVelocity = movement.x * speed;
        rb.linearVelocity = new Vector2(horizontalVelocity, rb.linearVelocity.y);
    }

    // ===================== NHẢY =====================
    protected virtual void HandleJumpInput()
    {
        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpForce);
            animator.SetTrigger("Jump");
        }
    }

    // ===================== TẤN CÔNG =====================
    protected virtual void HandleAttackInput()
    {
        if (Time.time >= nextAttackTime)
        {
            if (Input.GetButtonDown("Fire1")) // Fire1 = chuột trái hoặc Ctrl
            {
                Attack();
                nextAttackTime = Time.time + attackCooldown;
            }
        }
    }

    protected virtual void Attack()
    {
        // Phát trigger animation
        animator.SetTrigger("Attack");

        // Kiểm tra va chạm trong vùng tấn công
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, enemyLayers);

        foreach (Collider2D enemy in hitEnemies)
        {
            Debug.Log("Đánh trúng: " + enemy.name);
            // TODO: Gọi hàm nhận damage ở enemy
            // enemy.GetComponent<Enemy>().TakeDamage(damage);
        }
    }

    // ===================== KIỂM TRA CHẠM ĐẤT =====================
    protected virtual void CheckGrounded()
    {
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, checkRadius, groundLayer);
    }

    // ===================== XOAY NHÂN VẬT =====================
    protected virtual void FlipCharacter(float horizontalInput)
    {
        if (horizontalInput > 0 && !facingRight)
            Flip();
        else if (horizontalInput < 0 && facingRight)
            Flip();
    }

    protected virtual void Flip()
    {
        facingRight = !facingRight;
        Vector3 scale = transform.localScale;
        scale.x *= -1;
        transform.localScale = scale;
    }

    // ===================== CẬP NHẬT ANIMATION =====================
    protected virtual void UpdateAnimator()
    {
        animator.SetBool("isIdle", movement.x == 0);
        animator.SetBool("isGrounded", isGrounded);
        animator.SetFloat("yVelocity", rb.linearVelocity.y);
    }

    protected virtual void OnDrawGizmosSelected()
    {
        if (groundCheck != null)
        {
            Gizmos.color = Color.green;
            Gizmos.DrawWireSphere(groundCheck.position, checkRadius);
        }

        if (attackPoint != null)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(attackPoint.position, attackRange);
        }
    }
}
