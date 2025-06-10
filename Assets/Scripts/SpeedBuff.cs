using UnityEngine;

public class SpeedBuff : MonoBehaviour
{
    [SerializeField] private float speedMultiplier = 1.5f;
    [SerializeField] private float duration = 3f;

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Platform")) return;

        PlatformMover[] movers = FindObjectsByType<PlatformMover>(FindObjectsSortMode.None);
        foreach (var mover in movers)
        {
            mover.StartCoroutine(TemporarySpeedBoost(mover));
        }

        Destroy(gameObject);
    }

    private System.Collections.IEnumerator TemporarySpeedBoost(PlatformMover mover)
    {
        float originalSpeed = mover.moveSpeed;
        mover.moveSpeed *= speedMultiplier;
        yield return new WaitForSeconds(duration);
        mover.moveSpeed = originalSpeed;
    }
}
