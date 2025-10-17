using UnityEngine;

public class TestingRaycast2D : MonoBehaviour
{
    [SerializeField] protected Animator animator;
    [SerializeField] protected Weapon weapon;

    private void Awake()
    {
        if (animator == null)
        {
            animator = GetComponent<Animator>();
        }
        if (weapon == null)
        {
            weapon = GetComponentInChildren<Weapon>();
        }
    }

    private void Start()
    {
        animator.SetBool("isWalking", false);
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0)) // Left mouse button clicked
        {
            animator.SetTrigger("isShooting");
            weapon.Shoot();
        }
    }

    public virtual void CanShoot()
    {
        // This method can be expanded to include logic that determines if the character can shoot
    }
}

