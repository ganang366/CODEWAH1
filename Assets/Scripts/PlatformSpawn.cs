using UnityEngine;

public class PlatformSpawner : MonoBehaviour
{
    [Header("Prefab & Spawn Settings")]
    [SerializeField] private GameObject platformPrefab;
    [SerializeField] private int initialPlatformCount = 5;
    [SerializeField] private float platformLength = 10f;
    [SerializeField] private float spawnZStart = 0f;
    [SerializeField] private float spawnDistance = 50f;

    private float currentSpawnZ;

    void Start()
    {
        currentSpawnZ = spawnZStart;

        for (int i = 0; i < initialPlatformCount; i++)
        {
            SpawnPlatform();
        }
    }

    void Update()
    {
        // Selalu cek jika ujung platform sudah mendekati spawnDistance, spawn platform baru
        if (currentSpawnZ - Camera.main.transform.position.z < spawnDistance)
        {
            SpawnPlatform();
        }
    }

    void SpawnPlatform()
    {
        if (platformPrefab == null)
        {
            Debug.LogError("Platform Prefab belum diatur di Inspector.");
            return;
        }

        Vector3 spawnPos = new Vector3(0, 0, currentSpawnZ);
        Instantiate(platformPrefab, spawnPos, Quaternion.identity);
        currentSpawnZ += platformLength;
    }
}
