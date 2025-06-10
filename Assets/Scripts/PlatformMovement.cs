using UnityEngine;

public class PlatformMover : MonoBehaviour
{
    [SerializeField] public float moveSpeed = 5f;
    [SerializeField] private float destroyZ = -20f;

    void Update()
    {
        transform.Translate(Vector3.back * moveSpeed * Time.deltaTime);

        if (transform.position.z < destroyZ)
        {
            Destroy(gameObject);
        }
    }
}
