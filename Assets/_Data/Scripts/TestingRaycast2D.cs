using UnityEngine;

public class TestingRaycast2D : TBehaviour
{
    [SerializeField] protected Animator animator;
    [SerializeField] protected Weapon weapon;

    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.LoadAnimator();
        this.LoadWeapon();
    }

    protected virtual void LoadAnimator()
    {
        if (animator == null) return;
        this.animator = GetComponent<Animator>();
        Debug.Log(transform.name + ": LoadAnimator", gameObject);
    }
    protected virtual void LoadWeapon()
    {
        if (weapon == null) return;
        this.weapon = GetComponentInChildren<Weapon>();
        Debug.Log(transform.name + ": LoadWeapon", gameObject);
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

