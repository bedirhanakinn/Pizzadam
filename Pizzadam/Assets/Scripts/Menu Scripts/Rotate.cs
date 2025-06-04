using UnityEngine;

public class Rotate : MonoBehaviour
{
    public float angle = -8f;          // Rotation angle in degrees
    public float rotationSpeed = 60f;  // Degrees per second

    private float targetZ;
    private bool rotatingToMin = true;

    private void Start()
    {
        targetZ = angle;
    }

    private void Update()
    {
        Vector3 currentRotation = transform.eulerAngles;
        float currentZ = NormalizeAngle(currentRotation.z);
        float newZ = Mathf.MoveTowardsAngle(currentZ, targetZ, rotationSpeed * Time.deltaTime);

        transform.rotation = Quaternion.Euler(currentRotation.x, currentRotation.y, newZ);

        if (Mathf.Abs(NormalizeAngle(newZ - targetZ)) < 0.1f)
        {
            // Toggle target angle
            rotatingToMin = !rotatingToMin;
            targetZ = rotatingToMin ? angle : 0f;
        }
    }

    // Ensures angle is between -180 and 180
    private float NormalizeAngle(float angle)
    {
        while (angle > 180f) angle -= 360f;
        while (angle < -180f) angle += 360f;
        return angle;
    }
}
