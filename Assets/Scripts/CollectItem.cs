using UnityEngine;

public class CollectItem : MonoBehaviour
{
    public int value = 1;

    [Header("Effect & Sound")]
    public GameObject collectEffect;
    public AudioClip collectSound;

    private GameManager gm;

    void Start()
    {
        gm = FindFirstObjectByType<GameManager>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            // เพิ่มคะแนน
            gm.AddScore(value);

            // 🔊 เล่นเสียง
            if (collectSound != null)
            {
                AudioSource.PlayClipAtPoint(collectSound, transform.position);
            }

            // ✨ สร้างเอฟเฟค
            if (collectEffect != null)
            {
                GameObject fx = Instantiate(collectEffect, transform.position, Quaternion.identity);
Destroy(fx, 0.5f);
            }

            // ❌ ลบไอเทม
            Destroy(gameObject);
        }
    }
}