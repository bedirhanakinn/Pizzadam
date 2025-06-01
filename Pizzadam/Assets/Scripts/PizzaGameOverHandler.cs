using UnityEngine;
using UnityEngine.SceneManagement;

public class PizzaGameOverHandler : MonoBehaviour
{
    public GameObject pizzaGameOverPanel;

    private bool isGameOver = false;

    void Start()
    {
        pizzaGameOverPanel.SetActive(false);
        Time.timeScale = 1f;
    }

    public void HandlePizzaGameOver()
    {
        if (isGameOver) return;

        isGameOver = true;
        pizzaGameOverPanel.SetActive(true);
        Time.timeScale = 0f;
    }

    public void RetryPizzaGame()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void GoToPizzaMainMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("MainMenu"); // Replace if your main menu has a different name
    }
}
