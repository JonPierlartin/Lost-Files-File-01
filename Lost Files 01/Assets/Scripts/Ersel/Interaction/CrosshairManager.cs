using UnityEngine;

public class CrosshairManager : MonoBehaviour
{
    [Header("Crosshair Objeleri")]
    [SerializeField] private GameObject emptyCrosshair;
    [SerializeField] private GameObject filledCrosshair;

    [Header("Raycast Ayarları")]
    [SerializeField] private Camera playerCamera;
    [SerializeField] private float interactRange = 2f;
    [SerializeField] private LayerMask interactableLayer;

    [Header("Ayarlar")]
    [SerializeField] private float originOffset = 0.2f; // Kameradan biraz öne alınsın (kendini vurmayı engeller)
    [SerializeField] private bool enableDebug = false;

    private bool isFilledActive = false; // Mevcut durum

    private void Start()
    {
        // Başlangıçta GameObjectlerin aktiflik durumunu isFilledActive ile senkronize et.
        // Böylece inspector'da yanlış ayarlı olsalar bile kod doğru başlar.
        isFilledActive = filledCrosshair != null && filledCrosshair.activeSelf;

        // Eğer başlangıçta dolu görünmemesini istiyorsak zorla ayarla:
        // (isteğe göre yorum satırını kaldır)
        isFilledActive = false;
        if (filledCrosshair) filledCrosshair.SetActive(isFilledActive);
        if (emptyCrosshair) emptyCrosshair.SetActive(!isFilledActive);
    }

    void Update()
    {
        bool lookingAtInteractable = false;
        RaycastHit hit;

        // Raycast origin'ini hafifçe öne al (kamera pozisyonundan küçük bir offset)
        Vector3 origin = playerCamera.transform.position + playerCamera.transform.forward * originOffset;
        Vector3 dir = playerCamera.transform.forward;

        if (Physics.Raycast(origin, dir, out hit, interactRange, interactableLayer.value, QueryTriggerInteraction.Ignore))
        {
            // Eğer gerçekten IInteractable komponenti içeriyorsa işaretle
            if (hit.collider != null && hit.collider.TryGetComponent<IInteractable>(out var _))
            {
                lookingAtInteractable = true;
                if (enableDebug) Debug.DrawRay(origin, dir * interactRange, Color.green);
            }
            else
            {
                if (enableDebug) Debug.DrawRay(origin, dir * interactRange, Color.red);
            }

            if (enableDebug) Debug.Log($"Ray hit: {hit.collider.name} (layer {LayerMask.LayerToName(hit.collider.gameObject.layer)})");
        }
        else
        {
            if (enableDebug) Debug.DrawRay(origin, dir * interactRange, Color.yellow);
        }

        // Durum değiştiyse sadece o zaman aktif/pasif yap
        if (lookingAtInteractable != isFilledActive)
        {
            isFilledActive = lookingAtInteractable;

            if (filledCrosshair) filledCrosshair.SetActive(isFilledActive);
            if (emptyCrosshair) emptyCrosshair.SetActive(!isFilledActive);
        }
    }
}



