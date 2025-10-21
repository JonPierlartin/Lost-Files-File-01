using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 5f;
    private CharacterController controller;
    private Vector3 velocity;

    public Transform cameraTransform; // Inspector'da Main Camera
    public float mouseSensitivity = 100f;

    private float xRotation = 0f; // Kamera yukarı/aşağı dönüşü için

    void Start()
    {
        controller = GetComponent<CharacterController>();
        Cursor.lockState = CursorLockMode.Locked; // Mouse imlecini ortala ve gizle
    }

    void Update()
    {
        // Mouse ile bakış
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f); // Yukarı/aşağı sınır

        cameraTransform.localRotation = Quaternion.Euler(xRotation, 0f, 0f); // Kamera yukarı/aşağı
        transform.Rotate(Vector3.up * mouseX); // Karakter sadece yatay dönsün

        // Hareket
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        Vector3 move = transform.right * x + transform.forward * z;
        controller.Move(move * moveSpeed * Time.deltaTime);

        // Yerçekimi
        if(controller.isGrounded)
            velocity.y = 0f;
        else
            velocity.y += Physics.gravity.y * Time.deltaTime;

        controller.Move(velocity * Time.deltaTime);
    }
}
