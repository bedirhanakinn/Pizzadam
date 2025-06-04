using UnityEngine;

public class ImageLooper : MonoBehaviour
{
    public float moveSpeed = 200f; // Units per second
    public float leftX = -600f;
    public float rightX = 568f;

    private RectTransform rectTransform;
    private Vector3 targetPosition;

    private void Start()
    {
        rectTransform = GetComponent<RectTransform>();
        targetPosition = new Vector3(leftX, rectTransform.anchoredPosition.y, 0f);
    }

    private void Update()
    {
        rectTransform.anchoredPosition = Vector3.MoveTowards(
            rectTransform.anchoredPosition,
            targetPosition,
            moveSpeed * Time.deltaTime
        );

        if (Vector3.Distance(rectTransform.anchoredPosition, targetPosition) < 1f)
        {
            // Swap direction
            float newX = (Mathf.Approximately(targetPosition.x, leftX)) ? rightX : leftX;
            targetPosition = new Vector3(newX, rectTransform.anchoredPosition.y, 0f);
        }
    }
}
