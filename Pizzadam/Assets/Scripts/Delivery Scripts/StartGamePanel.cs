using UnityEngine;

public class StartGamePanel : MonoBehaviour
{
    public GameObject startPanel;

    private bool gameStarted = false;

    void Awake()
    {
        // Pause the game before anything else runs
        Time.timeScale = 0f;

        if (startPanel != null)
            startPanel.SetActive(true);
    }

    void Update()
    {
        // Check for tap or click to start
        if (!gameStarted && Input.GetMouseButtonDown(0))
        {
            StartGame();
        }
    }

    void StartGame()
    {
        gameStarted = true;
        Time.timeScale = 1f;

        if (startPanel != null)
            startPanel.SetActive(false);
    }
}
