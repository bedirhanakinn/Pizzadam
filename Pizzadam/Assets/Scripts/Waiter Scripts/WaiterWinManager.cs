using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.SceneManagement;


public class WaiterWinManager : MonoBehaviour
{
    [Header("Win Settings")]
    public int targetFoodCount = 5;           // How many foods need to be served
    public float introDuration = 2f;          // How long to show intro panel (in real seconds)

    [Header("UI References")]
    public GameObject introPanel;             // Panel with your goal image
    public GameObject winPanel;               // Win screen panel
    public Image progressImage;               // UI Image that shows progress
    public Sprite[] progressSprites;          // One sprite per delivery count

    private int currentFoodCount = 0;
    private bool hasWon = false;

    private void Start()
    {
        StartCoroutine(ShowIntroPanel());
    }

    private IEnumerator ShowIntroPanel()
    {
        Time.timeScale = 0f;

        if (introPanel != null)
        {
            introPanel.SetActive(true);

            // Ensure CanvasGroup is visible
            CanvasGroup canvasGroup = introPanel.GetComponent<CanvasGroup>();
            if (canvasGroup != null)
            {
                canvasGroup.alpha = 1f;
            }
        }

        yield return new WaitForSecondsRealtime(introDuration);
        BeginGame();
    }

    private void BeginGame()
    {
        if (introPanel != null)
        {
            CanvasGroup canvasGroup = introPanel.GetComponent<CanvasGroup>();
            if (canvasGroup != null)
            {
                StartCoroutine(FadeOutAndHide(canvasGroup));
            }
            else
            {
                introPanel.SetActive(false);
                Time.timeScale = 1f;
            }
        }
        else
        {
            Time.timeScale = 1f;
        }
    }

    private IEnumerator FadeOutAndHide(CanvasGroup canvasGroup)
    {
        float duration = 1f;
        float elapsed = 0f;

        while (elapsed < duration)
        {
            elapsed += Time.unscaledDeltaTime;
            canvasGroup.alpha = Mathf.Lerp(1f, 0f, elapsed / duration);
            yield return null;
        }

        canvasGroup.alpha = 0f;
        introPanel.SetActive(false);
        Time.timeScale = 1f;
    }

    public void RegisterSuccessfulDelivery()
    {
        if (hasWon)
            return;

        currentFoodCount++;

        // Update progress UI
        if (progressImage != null && progressSprites != null && currentFoodCount - 1 < progressSprites.Length)
        {
            progressImage.sprite = progressSprites[currentFoodCount - 1];
        }

        if (currentFoodCount >= targetFoodCount)
        {
            hasWon = true;
            ShowWinScreen();
        }
    }

    private void ShowWinScreen()
    {
        if (winPanel != null)
        {
            winPanel.SetActive(true);
        }

        Time.timeScale = 0f;
    }

    public void LoadMenuScene()
    {
        Time.timeScale = 1f; // Just in case it's still paused
        SceneManager.LoadScene("Menu 2");
    }

}
