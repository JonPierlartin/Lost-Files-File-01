using UnityEngine;
using UnityEngine.UI;

public class GeneratorInteraction : MonoBehaviour, IInteractable
{
    [Header("References")]
    public GameObject barUI;
    public Image fillImage;
    public Transform generatorCamPoint;
    public GameObject player;
    public Camera mainCam;
    public GameObject cursor;

    [Header("Bar Settings")]
    public float fillIncreasePerPress = 0.1f;
    public float minDecaySpeed = 0.1f;
    public float maxDecaySpeed = 0.3f;

    private bool isInteracting = false;
    private bool generatorStarted = false;
    private float targetFill = 0f;

    private Quaternion originalCamRot;
    private Vector3 originalCamPos;

    private PlayerMovement playerMovement; // 🎯 Kamera hareketini yöneten script
    private bool cameraWasLocked = false;

    public void Interact()
    {
        if (generatorStarted || isInteracting) return;
        StartInteraction();
    }

    private void StartInteraction()
    {
        isInteracting = true;
        barUI.SetActive(true);
        cursor.SetActive(false);

        originalCamRot = mainCam.transform.rotation;
        originalCamPos = mainCam.transform.position;

        // 🎯 PlayerMovement script’ini bul ve kapat
        playerMovement = player.GetComponent<PlayerMovement>();
        if (playerMovement != null)
            playerMovement.enabled = false;

        // 🎯 Oyuncu hareket etmesin
        var controller = player.GetComponent<CharacterController>();
        if (controller != null)
            controller.enabled = false;

        // 🎯 Cursor ayarı
        cameraWasLocked = Cursor.lockState == CursorLockMode.Locked;
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = false;
    }

    private void Update()
    {
        if (!isInteracting) return;

        // Kamera jeneratöre geçiş
        mainCam.transform.position = Vector3.Lerp(mainCam.transform.position, generatorCamPoint.position, Time.deltaTime * 5f);
        mainCam.transform.rotation = Quaternion.Lerp(mainCam.transform.rotation, generatorCamPoint.rotation, Time.deltaTime * 5f);

        // X'e basıldıkça dolma
        if (Input.GetKeyDown(KeyCode.X))
        {
            targetFill += fillIncreasePerPress;
            targetFill = Mathf.Clamp01(targetFill);
        }

        // Zamanla azalma
        float dynamicDecay = Mathf.Lerp(minDecaySpeed, maxDecaySpeed, fillImage.fillAmount);
        targetFill -= Time.deltaTime * dynamicDecay;
        targetFill = Mathf.Clamp01(targetFill);

        fillImage.fillAmount = Mathf.Lerp(fillImage.fillAmount, targetFill, Time.deltaTime * 10f);

        // Dolduysa jeneratör çalışsın
        if (fillImage.fillAmount >= 0.99f)
        {
            GeneratorStartSuccess();
        }

        // Space ile iptal
        if (Input.GetKeyDown(KeyCode.Space))
        {
            StopInteraction();
        }
    }

    private void GeneratorStartSuccess()
    {
        generatorStarted = true;
        Debug.Log("✅ Jeneratör çalıştı!");
        StopInteraction();
    }

    private void StopInteraction()
    {
        isInteracting = false;
        barUI.SetActive(false);
        cursor.SetActive(true);

        // Kamera eski haline dönsün
        mainCam.transform.position = originalCamPos;
        mainCam.transform.rotation = originalCamRot;

        // 🎯 PlayerMovement ve CharacterController geri açılsın
        if (playerMovement != null)
            playerMovement.enabled = true;

        var controller = player.GetComponent<CharacterController>();
        if (controller != null)
            controller.enabled = true;

        // 🎯 Cursor durumu geri gelsin
        if (cameraWasLocked)
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }
        else
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }

        fillImage.fillAmount = 0f;
        targetFill = 0f;
    }

    public bool IsGeneratorActive()
    {
        return generatorStarted;
    }

    public void ForceTurnOff()
    {
        if (generatorStarted)
        {
            generatorStarted = false;
            Debug.Log("⚙️ Jeneratör kapatıldı (dış etken).");
        }
    }
}