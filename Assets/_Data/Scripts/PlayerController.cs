using UnityEngine;

public class PlayerController : TBehaviour
{
    [SerializeField] protected Rigidbody2D rb;
    [SerializeField] Animator animator;
    [SerializeField] Vector2 movement;
    [SerializeField] float speed= 2.5f;

    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.LoadRigidbody2D();
        this.LoadAnimator();
    }
    public virtual void LoadRigidbody2D()
    {
        if (rb == null) return;
        this.rb = GetComponent<Rigidbody2D>();
        Debug.Log(transform.name + ": LoadRigidbody2D", gameObject);

    }

    public virtual void LoadAnimator()
    {
        if (animator == null) return;
        this.animator = GetComponent<Animator>();
        Debug.Log(transform.name + ": LoadAnimator", gameObject);
    }

    protected virtual void Update()
    {
       float horizontalInput = Input.GetAxisRaw("Horizontal");
       movement = new Vector2(horizontalInput, 0f).normalized;
    }

    protected virtual void FixedUpdate()
    {
        float horizontalVelocity = movement.x * speed;
        rb.linearVelocity = new Vector2(horizontalVelocity, rb.linearVelocity.y);
    }

    protected virtual void FixedLateUpdate()
    {
        if (movement.x != 0)
        {
            animator.SetBool("isWalking", true);
        }
        else
        {
            animator.SetBool("isWalking", false);
        }
    }
}
