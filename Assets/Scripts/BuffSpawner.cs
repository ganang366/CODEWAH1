using UnityEngine;

public class BuffSpawner : MonoBehaviour
{
    public static BuffSpawner Instance { get; private set; }

    [SerializeField] private GameObject speedBuffPrefab;

    void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
    }

    public void SpawnBuffOnPlatform(Vector3 position)
    {
        if (speedBuffPrefab != null)
        {
            Instantiate(speedBuffPrefab, position, Quaternion.identity);
        }
    }
}
