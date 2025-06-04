using UnityEngine;

public class RoadScroller : MonoBehaviour
{
    [HideInInspector] public float scrollSpeed;

    void Update()
    {
        transform.Translate(Vector3.forward * scrollSpeed * Time.deltaTime);
    }
}
