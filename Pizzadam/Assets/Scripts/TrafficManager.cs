using UnityEngine;

public class TrafficManager : MonoBehaviour
{
    [Header("Spawner References")]
    public Transform leftSpawner;
    public Transform rightSpawner;

    [Header("Prefabs")]
    public GameObject[] vehiclePrefabs; // Add Car & Bus prefabs here

    [Header("Spawn Timing")]
    public float startDelay = 1f;
    public float initialSpawnInterval = 2f;
    public float minSpawnInterval = 0.5f;
    public float difficultyRampRate = 0.05f; // Reduces interval per second

    private float spawnTimer;
    private float currentSpawnInterval;

    void Start()
    {
        currentSpawnInterval = initialSpawnInterval;
        spawnTimer = startDelay;
    }

    void Update()
    {
        spawnTimer -= Time.deltaTime;
        currentSpawnInterval = Mathf.Max(minSpawnInterval, currentSpawnInterval - difficultyRampRate * Time.deltaTime);

        if (spawnTimer <= 0f)
        {
            SpawnVehicle();
            spawnTimer = currentSpawnInterval;
        }
    }

    void SpawnVehicle()
    {
        bool spawnLeft = Random.value < 0.5f;

        // Only one spawner is used per spawn cycle
        Transform spawner = spawnLeft ? leftSpawner : rightSpawner;

        int vehicleIndex = Random.Range(0, vehiclePrefabs.Length);
        GameObject prefab = vehiclePrefabs[vehicleIndex];

        Quaternion spawnRotation = Quaternion.Euler(-90f, 0f, -90f);
        Instantiate(prefab, spawner.position, spawnRotation);

    }
}
