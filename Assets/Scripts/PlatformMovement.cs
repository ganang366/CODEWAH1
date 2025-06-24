using UnityEngine;

public class PlatformMover : MonoBehaviour
{
    [Header("Movement Settings")]
    public float moveSpeed = 5f;         // Kecepatan mundur (ke arah -Z)
    public float destroyZ = -20f;        // Titik di mana object dihancurkan

    void Update()
    {
        // Gerakkan object ke belakang
        transform.Translate(Vector3.back * moveSpeed * Time.deltaTime);

        // Hancurkan object jika terlalu jauh di belakang
        if (transform.position.z < destroyZ)
        {
            Destroy(gameObject);
        }
    }
}

