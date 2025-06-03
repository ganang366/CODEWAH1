using UnityEngine;

public class PlatformMover : MonoBehaviour
{
    [Header("Gerakan Platform")]
    [SerializeField] private Vector3 moveDirection = Vector3.back;
    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private float destroyZ = -20f;

    void Update()
    {
        transform.Translate(moveDirection.normalized * moveSpeed * Time.deltaTime);

        if (transform.position.z < destroyZ)
        {
            Destroy(gameObject);
        }
    }
}
