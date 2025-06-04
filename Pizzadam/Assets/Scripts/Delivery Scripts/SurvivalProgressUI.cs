using UnityEngine;
using UnityEngine.UI;

public class SurvivalProgressUI : MonoBehaviour
{
    [Header("Helmet Progress Settings")]
    public RectTransform helmetIcon;
    public RectTransform progressBar;
    public float totalSurvivalTime = 30f;

    [Header("Win/GameOver Panels")]
    public GameObject winPanel;
    public GameObject gameOverPanel;

    [Header("X-Axis Range")]
    public float startX = -380f;
    public float endX = 295f;

    private float timer = 0f;
    private bool tracking = true;

    void Start()
    {
        SetHelmetVisible(true);
    }

    void Update()
    {
        if (!tracking) return;

        if (winPanel.activeSelf || gameOverPanel.activeSelf)
        {
            SetHelmetVisible(false);
            tracking = false;
            return;
        }

        timer += Time.deltaTime;
        float progress = Mathf.Clamp01(timer / totalSurvivalTime);

        Vector3 newPos = helmetIcon.anchoredPosition;
        newPos.x = Mathf.Lerp(startX, endX, progress);
        helmetIcon.anchoredPosition = newPos;
    }

    void SetHelmetVisible(bool visible)
    {
        helmetIcon.gameObject.SetActive(visible);
        progressBar.gameObject.SetActive(visible);
    }
}
