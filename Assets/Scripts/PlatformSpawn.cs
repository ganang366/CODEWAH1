using UnityEngine;
using System.Collections.Generic;

public class SimplePlatformSpawner : MonoBehaviour
{
    [Header("Setup")]
    public Transform player;
    public GameObject platformPrefab;

    [Header("Platform Settings")]
    public float platformSpacing = 10f;
    public float spawnAhead = 50f;
    public float destroyBehind = 30f;

    [Header("Buff Settings")]
    public GameObject speedBuffPickupPrefab;
    public int minPlatformBetweenBuff = 5;
    public int maxPlatformBetweenBuff = 8;
    public float laneDistance = 3f; // Jarak antar jalur (kiri = -lane, tengah = 0, kanan = +lane)

    [Header("Debug")]
    public bool showDebugInfo = true;
    public bool forceSpawnOnStart = true;

    private List<GameObject> platforms = new List<GameObject>();
    private int nextBuffSpawnIn = 0;
    private int platformSpawnCounter = 0;

    void Start()
    {
        if (player == null)
        {
            Debug.LogError("❌ PLAYER belum di-assign di Inspector!");
            return;
        }

        if (platformPrefab == null)
        {
            Debug.LogError("❌ PLATFORM PREFAB belum di-assign di Inspector!");
            return;
        }

        // Tentukan jarak buff pertama
        nextBuffSpawnIn = Random.Range(minPlatformBetweenBuff, maxPlatformBetweenBuff + 1);

        // Spawn platform awal
        if (forceSpawnOnStart)
        {
            for (int i = 0; i < 10; i++)
            {
                float spawnZ = player.position.z + (i * platformSpacing);
                SpawnPlatform(spawnZ);
            }
        }
    }

    void Update()
    {
        CheckSpawn();
        CheckDestroy();

        if (showDebugInfo && Time.time % 1f < 0.02f)
        {
            Debug.Log($"📊 Platform count: {platforms.Count} | Next buff in: {nextBuffSpawnIn - platformSpawnCounter}");
        }
    }

    void CheckSpawn()
    {
        if (platforms.Count == 0) return;

        GameObject lastPlatform = platforms[platforms.Count - 1];
        if (lastPlatform == null) return;

        float lastZ = lastPlatform.transform.position.z;
        float playerZ = player.position.z;

        if ((lastZ - playerZ) < spawnAhead)
        {
            float newZ = lastZ + platformSpacing;
            SpawnPlatform(newZ);

            if (showDebugInfo)
                Debug.Log($"✨ Platform spawned at Z: {newZ}");
        }
    }

    void SpawnPlatform(float zPos)
    {
        Vector3 spawnPos = new Vector3(0, 0, zPos);
        GameObject newPlatform = Instantiate(platformPrefab, spawnPos, Quaternion.identity);
        newPlatform.name = $"Platform_{zPos:F0}";
        newPlatform.transform.SetParent(transform);
        platforms.Add(newPlatform);

        platformSpawnCounter++;

        // Spawn buff pickup jika waktunya
        if (speedBuffPickupPrefab != null && platformSpawnCounter >= nextBuffSpawnIn)
        {
            int lane = Random.Range(0, 3); // 0 = kiri, 1 = tengah, 2 = kanan
            float laneX = (lane - 1) * laneDistance;

            Vector3 buffPos = new Vector3(laneX, spawnPos.y + 1.5f, spawnPos.z);
            GameObject buff = Instantiate(speedBuffPickupPrefab, buffPos, Quaternion.identity);
            buff.transform.localScale = Vector3.one; // Hindari gepeng
            buff.name = $"Buff_{spawnPos.z:F0}";

            if (showDebugInfo)
                Debug.Log($"⚡ Buff spawned at lane {lane} (X={laneX}) on Z={spawnPos.z}");

            platformSpawnCounter = 0;
            nextBuffSpawnIn = Random.Range(minPlatformBetweenBuff, maxPlatformBetweenBuff + 1);
        }
    }

    void CheckDestroy()
    {
        for (int i = platforms.Count - 1; i >= 0; i--)
        {
            if (platforms[i] == null)
            {
                platforms.RemoveAt(i);
                continue;
            }

            float distanceBehind = player.position.z - platforms[i].transform.position.z;
            if (distanceBehind > destroyBehind)
            {
                if (showDebugInfo)
                    Debug.Log($"💥 Destroying platform at Z: {platforms[i].transform.position.z}");

                Destroy(platforms[i]);
                platforms.RemoveAt(i);
            }
        }
    }
}
