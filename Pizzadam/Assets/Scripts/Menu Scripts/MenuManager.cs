using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    [Header("Panels")]
    public GameObject mainMenuPanel;
    public GameObject levelSelectPanel;
    public GameObject rulesPanel;
    public GameObject settingsPanel;
    public GameObject seeRewardPanel;
    public GameObject creditsPanel;

    [Header("Audio")]
    public AudioSource buttonClickSound;

    private void Start()
    {
        ShowMainMenu();
        MenuAudioManager.Instance.PlayMusic(); // Start background music
    }

    // === PANEL SWITCH METHODS ===

    public void ShowMainMenu()
    {
        PlayClick();
        DisableAllPanels();
        mainMenuPanel.SetActive(true);
    }

    public void ShowLevelSelect()
    {
        PlayClick();
        DisableAllPanels();
        levelSelectPanel.SetActive(true);
    }

    public void ShowRules()
    {
        PlayClick();
        DisableAllPanels();
        rulesPanel.SetActive(true);
    }

    public void ShowSettings()
    {
        PlayClick();
        DisableAllPanels();
        settingsPanel.SetActive(true);
    }

    public void ShowSeeReward()
    {
        PlayClick();
        DisableAllPanels();
        seeRewardPanel.SetActive(true);
    }

    public void ShowCredits()
    {
        PlayClick();
        DisableAllPanels();
        creditsPanel.SetActive(true);
    }

    // === SCENE TRANSITIONS ===

    public void GoToDriverScene()
    {
        PlayClick();
        SceneManager.LoadScene("Driver Scene");
    }

    public void ResetMenuScene()
    {
        PlayClick();
        SceneManager.LoadScene("Menu 1");
    }

    public void GoToWaiterScene()
    {
        PlayClick();
        SceneManager.LoadScene("Waiter Scene");
    }


    // === UTILITY ===

    private void DisableAllPanels()
    {
        mainMenuPanel.SetActive(false);
        levelSelectPanel.SetActive(false);
        rulesPanel.SetActive(false);
        settingsPanel.SetActive(false);
        seeRewardPanel.SetActive(false);
        creditsPanel.SetActive(false);
    }

    private void PlayClick()
    {
        if (buttonClickSound != null)
        {
            buttonClickSound.Play();
        }
    }
}
