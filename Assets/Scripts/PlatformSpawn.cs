using UnityEngine;
using System.Collections.Generic;

public class PlatformSpawner : MonoBehaviour
{
    [SerializeField] private GameObject platformPrefab;
    [SerializeField] private Transform player;
    [SerializeField] private int maxVisiblePlatforms = 5;
    [SerializeField] private float platformLength = 10f;
    [SerializeField] private float spawnTriggerDistance = 30f;

    private float nextSpawnZ;
    private Queue<GameObject> activePlatforms = new Queue<GameObject>();

    void Start()
    {
        nextSpawnZ = Mathf.Floor(player.position.z / platformLength) * platformLength;

        for (int i = 0; i < maxVisiblePlatforms; i++)
        {
            SpawnPlatform();
        }
    }

    void Update()
    {
        if (player == null)
        {
            Debug.LogWarning("Player belum diassign!");
            return;
        }

        if (player.position.z + spawnTriggerDistance >= nextSpawnZ)
        {
            SpawnPlatform();
        }
    }

    void SpawnPlatform()
    {
        Vector3 spawnPos = new Vector3(0, 0, nextSpawnZ);
        GameObject platform = Instantiate(platformPrefab, spawnPos, Quaternion.identity);
        activePlatforms.Enqueue(platform);
        nextSpawnZ += platformLength;

        if (activePlatforms.Count > maxVisiblePlatforms)
        {
            GameObject old = activePlatforms.Dequeue();
            Destroy(old);
        }
    }
}
