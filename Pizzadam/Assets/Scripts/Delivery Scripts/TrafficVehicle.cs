using UnityEngine;

public class TrafficVehicle : MonoBehaviour
{
    public float moveSpeed = 4f;
    public float destroyZ = 50f;

    void Update()
    {
        transform.Translate(Vector3.forward * moveSpeed * Time.deltaTime, Space.World);

        if (transform.position.z > destroyZ)
        {
            Destroy(gameObject);
        }
    }
}
