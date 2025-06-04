using UnityEngine;
using System.Collections.Generic;

public class RoadManager : MonoBehaviour
{
    [Header("Prefabs")]
    public GameObject tutorialRoadPrefab;
    public GameObject roadSegmentPrefab;

    [Header("Scroll Settings")]
    public float scrollSpeed = 2f;

    [Header("Road Positioning")]
    public float roadLength = 23.4f; // Visual length of each segment

    private List<GameObject> roadSegments = new List<GameObject>();

    void Start()
    {
        // Spawn first (tutorial) segment at Z = 0
        GameObject first = Instantiate(tutorialRoadPrefab, new Vector3(0, 0, 0), Quaternion.identity);
        AssignScroller(first);
        roadSegments.Add(first);

        // Spawn second (normal) segment ahead at Z = 23.4
        GameObject second = Instantiate(roadSegmentPrefab, new Vector3(0, 0, roadLength), Quaternion.identity);
        AssignScroller(second);
        roadSegments.Add(second);
    }

    void Update()
    {
        for (int i = roadSegments.Count - 1; i >= 0; i--)
        {
            GameObject segment = roadSegments[i];

            if (segment.transform.position.z >= roadLength)
            {
                // Destroy the segment
                Destroy(segment);
                roadSegments.RemoveAt(i);

                // Spawn a new one at Z = -23.4
                Vector3 spawnPos = new Vector3(0, 0, -roadLength);
                GameObject newSegment = Instantiate(roadSegmentPrefab, spawnPos, Quaternion.identity);
                AssignScroller(newSegment);
                roadSegments.Add(newSegment);
            }
        }
    }

    void AssignScroller(GameObject road)
    {
        RoadScroller scroller = road.GetComponent<RoadScroller>();
        if (scroller != null)
        {
            scroller.scrollSpeed = scrollSpeed;
        }
    }
}
