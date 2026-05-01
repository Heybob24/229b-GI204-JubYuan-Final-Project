using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public float delay = 0.1f; // เวลาให้เสียงเล่น

    // ▶️ ไปหน้าเลือกด่าน
    public void LoadLevelSelect()
    {
        Time.timeScale = 1f;
        Invoke(nameof(LoadLevelSelectScene), delay);
    }

    void LoadLevelSelectScene()
    {
        SceneManager.LoadScene("LevelSelect");
    }

    // ▶️ โหลดฉากแบบใช้ชื่อ
    public void LoadScene(string sceneName)
    {
        Time.timeScale = 1f;
        StartCoroutine(LoadSceneDelay(sceneName));
    }

    System.Collections.IEnumerator LoadSceneDelay(string sceneName)
    {
        yield return new WaitForSecondsRealtime(delay);
        SceneManager.LoadScene(sceneName);
    }

    // ❌ Quit
    public void QuitGame()
    {
#if UNITY_WEBGL
        Debug.Log("Quit not supported on WebGL");
#else
        Application.Quit();
#endif

#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif
    }
}