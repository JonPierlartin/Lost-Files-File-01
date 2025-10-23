using UnityEngine;

public class Bed : MonoBehaviour, IInteractable
{
    [Header("Saklanma Ayarları")]
    [SerializeField] private Transform hidePosition;   // Yatağın altındaki nokta
    [SerializeField] private Transform exitPosition;   // Çıkış noktası
    [SerializeField] private float moveSpeed = 5f;

    [Header("Kamera Ayarları")]
    [SerializeField] private Camera playerCamera;      // Oyuncu kamerası
    [SerializeField] private float lookSpeed = 50f;    // Kamera dönüş hızı
    [SerializeField] private float maxLookAngle = 30f; // Sağa-sola bakış limiti (derece)

    private bool isHiding = false;
    private Transform player;
    private PlayerMovement playerMovement;

    private float currentYRotation = 0f;
    private float targetYRotation = 0f;
    private Quaternion baseCamRotation; // Yatağa girerkenki temel rotasyon

    private void Update()
    {
        if (isHiding)
        {
            HandleCameraLook();

            if (Input.GetKeyDown(KeyCode.Space))
                StartCoroutine(ExitFromBed());
        }
    }

    public void Interact()
    {
        if (player == null)
        {
            GameObject playerObj = GameObject.FindGameObjectWithTag("Player");
            if (playerObj != null)
            {
                player = playerObj.transform;
                playerMovement = playerObj.GetComponent<PlayerMovement>();
            }
        }

        if (!isHiding)
            StartCoroutine(HideUnderBed());
    }

    private System.Collections.IEnumerator HideUnderBed()
    {
        isHiding = true;
        Debug.Log("Yatağın altına saklanılıyor...");

        if (playerMovement != null)
            playerMovement.enabled = false;

        // Oyuncuyu yatağın altına taşı
        while (Vector3.Distance(player.position, hidePosition.position) > 0.05f)
        {
            player.position = Vector3.MoveTowards(player.position, hidePosition.position, moveSpeed * Time.deltaTime);
            yield return null;
        }

        // 🔹 Kamera yatağın ön tarafına bakacak şekilde hizalanır
        // Yani yatağın "ön" yönü (transform.forward) ile aynı hizada olacak
        playerCamera.transform.rotation = Quaternion.LookRotation(transform.forward, Vector3.up);

        // Temel bakış yönünü kaydet (oyuncunun kameraya göre sıfır noktası)
        baseCamRotation = playerCamera.transform.localRotation;
        currentYRotation = 0f;
        targetYRotation = 0f;

        Debug.Log("Oyuncu yatağın altında. Space ile çıkabilirsiniz.");
    }

    private System.Collections.IEnumerator ExitFromBed()
    {
        isHiding = false;
        Debug.Log("Yatağın altından çıkılıyor...");

        // Kamerayı sıfırla
        playerCamera.transform.localRotation = Quaternion.identity;

        // Oyuncuyu çıkış noktasına taşı
        while (Vector3.Distance(player.position, exitPosition.position) > 0.05f)
        {
            player.position = Vector3.MoveTowards(player.position, exitPosition.position, moveSpeed * Time.deltaTime);
            yield return null;
        }

        if (playerMovement != null)
            playerMovement.enabled = true;

        Debug.Log("Oyuncu yatağın altından çıktı.");
    }

    private void HandleCameraLook()
    {
        float mouseX = Input.GetAxis("Mouse X");

        targetYRotation += mouseX * lookSpeed * Time.deltaTime;
        targetYRotation = Mathf.Clamp(targetYRotation, -maxLookAngle, maxLookAngle);

        currentYRotation = Mathf.Lerp(currentYRotation, targetYRotation, Time.deltaTime * 10f);

        // 🔹 Kamera, yatağın ön tarafını referans alarak döner
        playerCamera.transform.localRotation = baseCamRotation * Quaternion.Euler(0f, currentYRotation, 0f);
    }
}




