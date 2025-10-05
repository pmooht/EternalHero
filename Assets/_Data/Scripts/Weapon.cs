using UnityEngine;

public class Weapon : MonoBehaviour
{
    [SerializeField] protected GameObject projectilePrefab;
    [SerializeField] protected GameObject Shooter;
    [SerializeField] protected Transform projectileSpawnPoint;

    public virtual void Awake()
    {
        projectileSpawnPoint = transform.Find("FirePoint");
    }
    void Start()
    {
      
    }
    
    public virtual void Shoot()
    {
        if (projectilePrefab != null && projectileSpawnPoint != null)
        {
            GameObject projectile = Instantiate(projectilePrefab, projectileSpawnPoint.position, Quaternion.identity);
            Rigidbody rb = projectile.GetComponent<Rigidbody>();
            if (rb != null)
            {
                rb.AddForce(Shooter.transform.forward * 1000f); // Adjust force as needed
            }
        }
    }
}
