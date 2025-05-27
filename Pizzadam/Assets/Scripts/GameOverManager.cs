using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverManager : MonoBehaviour
{
    public static GameOverManager Instance;
    public GameObject gameOverUI; // Assign your Game Over UI Panel here

    private bool isGameOver = false;

    void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }

    public void TriggerGameOver()
    {
        if (isGameOver) return;

        isGameOver = true;
        gameOverUI.SetActive(true);
        Time.timeScale = 0f; // Pause the game
    }

    public void RetryGame()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("Waiter Scene");
    }

    public void GoToMainMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("Boss Scene 2");
    }
}
