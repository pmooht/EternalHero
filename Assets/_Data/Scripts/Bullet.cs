using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] protected float speed = 2f;
    [SerializeField] protected float lifeTime = 5f;
    [SerializeField] protected Vector2 direction = Vector2.right;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Destroy(gameObject, lifeTime);
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 movement = direction.normalized * speed * Time.deltaTime;
        transform.Translate(movement);
    }
}
