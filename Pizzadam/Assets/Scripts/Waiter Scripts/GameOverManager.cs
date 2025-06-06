using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverManager : MonoBehaviour
{
    public static GameOverManager Instance;

    public GameObject gameOverUI;
    public GameObject movementJoystick; // 👈 Add this

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
        if (gameOverUI != null) gameOverUI.SetActive(true);
        if (movementJoystick != null) movementJoystick.SetActive(false); // 👈 Disable joystick
        Time.timeScale = 0f;
    }

    public void RetryGame()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("Waiter Scene");
    }

    public void GoToMainMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("Menu 1");
    }
}
