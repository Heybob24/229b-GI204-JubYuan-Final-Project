using UnityEngine;

public class Idle_Controller : MonoBehaviour
{
    public float speed = 5f;
    public float jumpForce = 5f;
    public int maxJumps = 99; // 1 = กระโดดได้ครั้งเดียว, 2 = Double Jump

    int jumpCount;

    float x, sx;
    Animator am;
    Rigidbody2D rb;

    void Start()
    {
        am = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        sx = transform.localScale.x;
    }

    void Update()
    {
        x = Input.GetAxis("Horizontal");

        am.SetFloat("Speed", Mathf.Abs(x));

        // ✅ จำกัดการกระโดด
        if (Input.GetButtonDown("Jump") && jumpCount < maxJumps)
        {
            jumpCount++;
            am.SetBool("Jump", true);
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
        }

        rb.velocity = new Vector2(x * speed, rb.velocity.y);

        // Flip
        if (x > 0)
            transform.localScale = new Vector3(sx, transform.localScale.y, transform.localScale.z);
        else if (x < 0)
            transform.localScale = new Vector3(-sx, transform.localScale.y, transform.localScale.z);
    }

    // ✅ รีเซ็ตเมื่อชนพื้น
    void OnCollisionEnter2D(Collision2D coll)
    {
        // ถ้าอยากให้เฉพาะพื้นจริง ๆ ให้เช็ค tag หรือ layer
        if (coll.gameObject.CompareTag("Ground"))
        {
            jumpCount = 0;
            am.SetBool("Jump", false);
        }
    }
}