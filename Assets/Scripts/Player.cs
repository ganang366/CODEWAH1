using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("Movement Settings")]
    public float laneDistance = 3f;      // Jarak antar jalur kiri-tengah-kanan
    public float moveSpeed = 10f;        // Kecepatan geser lateral
    public float jumpForce = 7f;

    private Rigidbody rb;
    private int currentLane = 1;          // 0 = kiri, 1 = tengah, 2 = kanan
    private bool isGrounded = true;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        // Geser ke kanan
        if (Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.RightArrow))
        {
            if (currentLane < 2)
            {
                currentLane++;
            }
        }

        // Geser ke kiri
        if (Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.LeftArrow))
        {
            if (currentLane > 0)
            {
                currentLane--;
            }
        }

        // Lompat
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            rb.AddForce(Vector3.up * jumpForce, ForceMode.VelocityChange);
            isGrounded = false;
        }

        // Pindah posisi lateral ke jalur target secara smooth
        Vector3 targetPosition = new Vector3((currentLane - 1) * laneDistance, transform.position.y, transform.position.z);
        Vector3 moveDir = Vector3.zero;
        moveDir.x = Mathf.Lerp(transform.position.x, targetPosition.x, moveSpeed * Time.deltaTime);
        transform.position = new Vector3(moveDir.x, transform.position.y, transform.position.z);
    }

    // Cek grounded via collision dengan platform
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Platform"))
        {
            isGrounded = true;
        }
    }
}
