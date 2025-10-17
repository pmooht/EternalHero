using System.Collections;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    [SerializeField] protected GameObject projectilePrefab;
    [SerializeField] protected GameObject Shooter;
    [SerializeField] protected Transform projectileSpawnPoint;
    [SerializeField] protected GameObject explosionEffect;
    [SerializeField] protected LineRenderer lineRenderer;

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

    public IEnumerator ShootWithRaycast()
    {
        if (explosionEffect != null && lineRenderer != null)
        {
           RaycastHit2D hitInfo = Physics2D.Raycast(projectileSpawnPoint.position, Shooter.transform.right);
            if (hitInfo)
            {
                // Instantiate explosion effect at hit point

                Instantiate(explosionEffect, hitInfo.point, Quaternion.identity);

                // Show line renderer
                lineRenderer.SetPosition(0, projectileSpawnPoint.position);
                lineRenderer.SetPosition(1, hitInfo.point);
            }
            else
            {
                 lineRenderer.SetPosition(0, projectileSpawnPoint.position);
                 lineRenderer.SetPosition(1, projectileSpawnPoint.position + (Vector3)(Shooter.transform.right * 100f)); // Arbitrary far point
            }
            lineRenderer.enabled = true;

            yield return null;

            lineRenderer.enabled = false;

        }
    }
}

