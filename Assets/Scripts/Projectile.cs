using UnityEngine;

public class Projectile : MonoBehaviour
{
    public Rigidbody2D rb;
    public float targetSpeed = 20f; 
    public float timeToReachSpeed = 0.1f; 
    public float direction = 1f;
    public int damage = 1;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Start()
    {
        
        float acceleration = targetSpeed / timeToReachSpeed;

        
        float mass = rb.mass;
        float forceMagnitude = mass * acceleration;

        
        Vector2 forceVector = new Vector2(direction, 0f).normalized * forceMagnitude;

        
        rb.AddForce(forceVector, ForceMode2D.Impulse);

        

        Destroy(gameObject, 5f);
    }

    void OnCollisionEnter2D(Collision2D coll)
    {
        
        if (coll.gameObject.CompareTag("Player"))
        {
            GameManager.instance.TakeDamage(damage);
        }

        if (coll.gameObject.CompareTag("Player") || coll.gameObject.CompareTag("Ground"))
        {
            Destroy(gameObject);
        }
    }
}