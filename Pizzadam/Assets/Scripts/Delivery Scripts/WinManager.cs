using UnityEngine;
using UnityEngine.SceneManagement;

public class WinManager : MonoBehaviour
{
    public float winTime = 30f;
    public GameObject winPanel;

    private float timer = 0f;
    private bool hasWon = false;

    void Start()
    {
        winPanel.SetActive(false);
        Time.timeScale = 1f;
    }

    void Update()
    {
        if (hasWon) return;

        timer += Time.deltaTime;

        if (timer >= winTime)
        {
            TriggerWin();
        }
    }

    void TriggerWin()
    {
        hasWon = true;
        winPanel.SetActive(true);
        Time.timeScale = 0f;

        FindObjectOfType<AudioManager>().PlaySuccessMusic();
    }

    public void GoToMainMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("Menu 2");
    }
}
