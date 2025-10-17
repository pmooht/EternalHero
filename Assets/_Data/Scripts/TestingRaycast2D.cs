using UnityEngine;

public class TestingRaycast2D : TBehaviour
{
    [SerializeField] protected Animator animator;
    [SerializeField] protected Weapon weapon;

    protected override void LoadComponents()
    {
        base.LoadComponents();
        if (animator == null)
        {
            animator = GetComponent<Animator>();
        }
        if (weapon == null)
        {
            weapon = GetComponentInChildren<Weapon>();
        }
    }

    protected override void Start()
    {
        
        base.Start();
        animator.SetBool("isWalking", false);
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0)) // Left mouse button clicked
        {
            animator.SetTrigger("isShooting");
        }
    }

    public virtual void CanShoot()
    {
        if (weapon != null)
        {
            StartCoroutine(weapon.ShootWithRaycast());
        }
    }
}

