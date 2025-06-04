using UnityEngine;

public class ScalePulse : MonoBehaviour
{
    public float minScale = 1.5f;
    public float maxScale = 1.7f;
    public float pulseSpeed = 0.1f; // How fast it pulses

    private Vector3 targetScale;
    private bool scalingUp = true;

    private void Start()
    {
        transform.localScale = Vector3.one * minScale;
        targetScale = Vector3.one * maxScale;
    }

    private void Update()
    {
        transform.localScale = Vector3.MoveTowards(
            transform.localScale,
            targetScale,
            pulseSpeed * Time.deltaTime
        );

        if (Vector3.Distance(transform.localScale, targetScale) < 0.01f)
        {
            scalingUp = !scalingUp;
            targetScale = scalingUp ? Vector3.one * maxScale : Vector3.one * minScale;
        }
    }
}
