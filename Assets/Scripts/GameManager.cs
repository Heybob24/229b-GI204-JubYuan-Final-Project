using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public int score = 0;
    public TextMeshProUGUI scoreText;

    public GameObject gameOverPanel;
    public GameObject winPanel;

    public GameObject scoreUI;
      public AudioSource bgmSource;

 public void StopBGM()
{
    if (bgmSource != null)
    {
        bgmSource.Stop();
    }
}

    void Awake()
    {
        instance = this;
    }

    void Start()
    {
        UpdateScoreUI();
    }

    public void AddScore(int amount)
    {
        score += amount;
        UpdateScoreUI();
    }

    void UpdateScoreUI()
    {
        scoreText.text = "X " + score;
    }

    public void GameOver()
    {
        gameOverPanel.SetActive(true);
        scoreUI.SetActive(false);
        Time.timeScale = 0f;
    }
    public void WinGame()
{
    winPanel.SetActive(true);
    scoreUI.SetActive(false);

    StopBGM(); // หยุดเพลงพื้นหลัง

   
  

    Time.timeScale = 0f; // หยุดเกม
}

    // Restart
    public void Retry()
{
    Time.timeScale = 1f; // กันเกมค้างจาก GameOver
    SceneManager.LoadScene(SceneManager.GetActiveScene().name);
}
}