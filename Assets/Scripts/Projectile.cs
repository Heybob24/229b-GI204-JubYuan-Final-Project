using UnityEngine;

public class Projectile : MonoBehaviour
{
    public Rigidbody2D rb;
    public float force = 20f;
    public float direction = 1f;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Start()
    {
        Vector2 dir = new Vector2(direction, 0f);

        rb.velocity = dir.normalized * force;

        Destroy(gameObject, 5f);
    }

    void OnCollisionEnter2D(Collision2D coll)
    {
        if (coll.gameObject.CompareTag("Player") || coll.gameObject.CompareTag("Ground"))
        {
            Destroy(gameObject);
        }
    }
}