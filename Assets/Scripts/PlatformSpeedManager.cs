using UnityEngine;
using System.Collections;

public class PlatformSpeedManager : MonoBehaviour
{
    public static PlatformSpeedManager Instance;

    public float baseSpeed = 5f;
    public float currentSpeed;

    void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
    }

    void Start()
    {
        currentSpeed = baseSpeed;
    }

    public void ApplySpeedBuff(float multiplier, float duration)
    {
        StopAllCoroutines();
        StartCoroutine(SpeedBuffRoutine(multiplier, duration));
    }

    IEnumerator SpeedBuffRoutine(float multiplier, float duration)
    {
        currentSpeed = baseSpeed * multiplier;
        yield return new WaitForSeconds(duration);
        currentSpeed = baseSpeed;
    }
}
