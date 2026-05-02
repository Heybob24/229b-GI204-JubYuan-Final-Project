using UnityEngine;

public class Idle_Controller : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float jumpForce = 5f;
    public int maxJumps = 2;

    private int jumpCount;
    private bool isDead = false;

    float x, sx;
    Animator am;
    Rigidbody2D rb;

    [Header("Audio")]
    public AudioSource audioSource;     // SFX (เดิน/กระโดด/ตาย)
    public AudioClip footstepSound;
    public AudioClip jumpSound;
    public AudioClip deathSound;
    public AudioClip winSound;
  
    bool isWalking = false;

    void Start()
    {
        am = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        sx = transform.localScale.x;
    }

    void Update()
    {
        if (isDead) return;

        x = Input.GetAxis("Horizontal");

        // 🎬 Animation
        am.SetFloat("Speed", Mathf.Abs(x));

        // 🚶 Move
        rb.velocity = new Vector2(x * moveSpeed, rb.velocity.y);

        // 👣 เสียงเดิน (loop)
        if (Mathf.Abs(x) > 0.1f)
        {
            if (!isWalking)
            {
                audioSource.clip = footstepSound;
                audioSource.loop = true;
                audioSource.Play();
                isWalking = true;
            }
        }
        else
        {
            if (isWalking)
            {
                audioSource.Stop();
                isWalking = false;
            }
        }

        // 🦘 Jump
        if (Input.GetButtonDown("Jump") && jumpCount < maxJumps)
        {
            jumpCount++;
            am.SetBool("Jump", true);
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);

            // 🔊 เสียงกระโดด (ไม่ทับ loop)
            audioSource.PlayOneShot(jumpSound);
        }

        // 🔄 Flip
        if (x > 0)
            transform.localScale = new Vector3(sx, transform.localScale.y, transform.localScale.z);
        else if (x < 0)
            transform.localScale = new Vector3(-sx, transform.localScale.y, transform.localScale.z);
    }

    void OnCollisionEnter2D(Collision2D coll)
    {
        if (coll.gameObject.CompareTag("Ground"))
        {
            jumpCount = 0;
            am.SetBool("Jump", false);
        }

        if (coll.gameObject.CompareTag("Obstacle"))
        {
            Die();
        }
    }

    void Die()
    {
        if (isDead) return;
        isDead = true;

        // ❌ หยุดทุกอย่าง
        rb.velocity = Vector2.zero;
        rb.bodyType = RigidbodyType2D.Static; // 🔥 สำคัญ (ไม่ขยับอีก)

        // ❌ ปิด Collider กันชนซ้ำ
        GetComponent<Collider2D>().enabled = false;

        // 🔇 หยุดเสียงเดิน
        audioSource.Stop();

        // 🔊 เสียงตาย
        audioSource.PlayOneShot(deathSound);

        // 🎬 Animation
        am.SetBool("Jump", false);
        am.SetTrigger("Die");

        // 🔇 ปิดเพลงพื้นหลัง
        GameManager.instance.StopBGM();

        Invoke(nameof(ShowGameOver), 1.5f);
    }
    public void Win()
{
    isDead = true; // 🔥 หยุด Update ทั้งหมด (สำคัญมาก)

    

    // 🔇 หยุดเสียงเดิน + reset state
    audioSource.Stop();
    isWalking = false;

    // 🔊 เล่นเสียงชนะ
    audioSource.loop = false;
    audioSource.PlayOneShot(winSound);

    // 🎬 (ถ้ามี animation ชนะ)
    am.SetBool("Jump", false);
    
}

    void ShowGameOver()
    {
        GameManager.instance.GameOver();
    }
}