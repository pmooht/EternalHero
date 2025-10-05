using UnityEngine;

public class InstatiatePrefab : MonoBehaviour
{
    [SerializeField] protected GameObject prefab;
    [SerializeField] protected Transform point;
    [SerializeField] protected float livingtime = 1f;

    public virtual void InstantiatePrefabAtPoint()
    {
        if (prefab != null && point != null)
        {
            GameObject instance = Instantiate(prefab, point.position, Quaternion.identity);
            Destroy(instance, livingtime);
        }
        else
        {
            Debug.LogWarning("Prefab or Point is not assigned.", this);
        }
    }

}
