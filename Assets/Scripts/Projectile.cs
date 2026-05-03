using UnityEngine;

public class Projectile : MonoBehaviour
{
    public Rigidbody2D rb;
    public float force = 20f;
    public float direction = 1f;

    public int damage = 1; // 🔥 เพิ่มดาเมจ

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
        // 💥 ถ้าโดน Player
        if (coll.gameObject.CompareTag("Player"))
        {
            // เรียก GameManager ลด HP
            GameManager.instance.TakeDamage(damage);
        }

        // 💥 ชนอะไรก็หาย
        if (coll.gameObject.CompareTag("Player") || coll.gameObject.CompareTag("Ground"))
        {
            Destroy(gameObject);
        }
    }
}