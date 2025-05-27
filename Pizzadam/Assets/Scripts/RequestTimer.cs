using UnityEngine;

public class RequestTimer : MonoBehaviour
{
    public float requestDuration = 10f; // Adjustable in Inspector
    public Transform timerBar; // Assign the TimerPlane (the visual bar)

    private float timeRemaining;
    private bool isTiming = true;

    void Start()
    {
        timeRemaining = requestDuration;
    }

    void Update()
    {
        if (!isTiming) return;

        timeRemaining -= Time.deltaTime;

        if (timerBar != null)
        {
            float xScale = Mathf.Clamp01(timeRemaining / requestDuration);
            timerBar.localScale = new Vector3(xScale, timerBar.localScale.y, timerBar.localScale.z);
        }

        if (timeRemaining <= 0f)
        {
            isTiming = false;
            GameOverManager.Instance.TriggerGameOver();
        }
    }

    // Optional: allow external scripts to cancel the timer (e.g., waiter delivers food)
    public void CancelTimer()
    {
        isTiming = false;
    }
}
