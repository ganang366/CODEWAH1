using UnityEngine;

public class FacePlayer : MonoBehaviour
{
    private Transform player;

    void Start()
    {
        GameObject playerObj = GameObject.FindGameObjectWithTag("Player");
        if (playerObj != null)
        {
            player = playerObj.transform;
            Debug.Log("✅ Player ditemukan oleh buff pickup.");
        }
        else
        {
            Debug.LogWarning("❌ Tidak menemukan Player dengan tag 'Player'.");
        }
    }

    void Update()
    {
        if (player == null) return;

        // Hitung arah tanpa pengaruh Y
        Vector3 direction = player.position - transform.position;
        direction.y = 0f;

        if (direction.sqrMagnitude > 0.01f)
        {
            Quaternion targetRotation = Quaternion.LookRotation(direction);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * 5f);
        }

        // (Opsional) efek berputar sambil menghadap
        transform.Rotate(Vector3.up * 100f * Time.deltaTime, Space.Self);
    }
}
