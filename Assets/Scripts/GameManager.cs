using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using System.Collections;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    [Header("Score")]
    public int score = 0;
    public TextMeshProUGUI scoreText;

    [Header("HP")]
    public int maxHP = 3;
    public int currentHP;
    public TextMeshProUGUI hpText;

    [Header("Hit System")]
    public float invincibleTime = 1f;
    private bool isInvincible = false;
    private bool isTakingDamage = false;

    [Header("Damage FX")]
    public AudioSource sfxSource;
    public AudioClip hitSound;
    public GameObject hitEffectPrefab;

    [Header("UI")]
    public GameObject gameOverPanel;
    public GameObject winPanel;
    public GameObject scoreUI;
    public GameObject hpUI;

    [Header("Sound")]
    public AudioSource bgmSource;

    private bool isGameOver = false;

    void Awake()
    {
        if (instance == null) instance = this;
        else Destroy(gameObject);
    }

    void Start()
    {
        currentHP = maxHP;
        Time.timeScale = 1f;

        UpdateScoreUI();
        UpdateHPUI();
    }

    // ================= SCORE =================
    public void AddScore(int amount)
    {
        score += amount;
        UpdateScoreUI();
    }

    void UpdateScoreUI()
    {
        if (scoreText != null)
            scoreText.text = "X " + score;
    }

    // ================= HP =================
    public void TakeDamage(int damage)
    {
        if (isInvincible || isTakingDamage || isGameOver) return;

        isTakingDamage = true;

        currentHP -= damage;
        if (currentHP < 0) currentHP = 0;

        UpdateHPUI();

        // เสียงโดน
        if (sfxSource != null && hitSound != null)
            sfxSource.PlayOneShot(hitSound);

        GameObject player = GameObject.FindGameObjectWithTag("Player");

        // เอฟเฟคโดน
        if (player != null && hitEffectPrefab != null)
{
    GameObject fx = Instantiate(hitEffectPrefab, player.transform.position, Quaternion.identity);
    Destroy(fx, 1f); // ⏱ หายใน 1 วิ
}

        // Knockback (เสริม)
        if (player != null)
        {
            Rigidbody2D rb = player.GetComponent<Rigidbody2D>();
            if (rb != null)
            {
                rb.AddForce(new Vector2(-2f, 2f), ForceMode2D.Impulse);
            }
        }

        if (currentHP <= 0)
        {
            if (player != null)
            {
                Idle_Controller ctrl = player.GetComponent<Idle_Controller>();
                if (ctrl != null)
                    ctrl.Die();
            }
            return;
        }

        StartCoroutine(InvincibleCoroutine());
        StartCoroutine(ResetDamageFlag());
    }

    IEnumerator ResetDamageFlag()
    {
        yield return null;
        isTakingDamage = false;
    }

    void UpdateHPUI()
    {
        if (hpText != null)
            hpText.text = "X " + currentHP;
    }

    IEnumerator InvincibleCoroutine()
    {
        isInvincible = true;

        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player != null)
        {
            SpriteRenderer sr = player.GetComponent<SpriteRenderer>();

            float timer = 0f;

            while (timer < invincibleTime)
            {
                if (sr != null)
                {
                    
                    sr.color = (sr.color == Color.white) ? Color.red : Color.white;
                }

                yield return new WaitForSeconds(0.1f);
                timer += 0.1f;
            }

            if (sr != null)
                sr.color = Color.white;
        }

        isInvincible = false;
    }

    // ================= GAME STATE =================
    public void GameOver()
    {
        if (isGameOver) return;
        isGameOver = true;

        if (gameOverPanel != null) gameOverPanel.SetActive(true);
        if (scoreUI != null) scoreUI.SetActive(false);
        if (hpUI != null) hpUI.SetActive(false);

        StopBGM();

        Time.timeScale = 0f;
    }

    public void WinGame()
    {
        if (winPanel != null) winPanel.SetActive(true);
        if (scoreUI != null) scoreUI.SetActive(false);
        if (hpUI != null) hpUI.SetActive(false);

        StopBGM();

        Time.timeScale = 0f;
    }

    public void StopBGM()
    {
        if (bgmSource != null)
            bgmSource.Stop();
    }

    public void Retry()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}