using UnityEngine;

public class Interactor : MonoBehaviour
{
    [Header("Etkileşim Ayarları")]
    [Tooltip("Oyuncunun ne kadar uzağa etkileşimde bulunabileceği.")]
    [SerializeField] private float interactionDistance = 3f;

    [Tooltip("Sadece bu layer'daki objelerle etkileşime girilecek.")]
    [SerializeField] private LayerMask interactableLayer;

    [SerializeField]private Camera fpsCamera;
    
    // O an baktığımız, etkileşime girilebilir objeyi hafızada tutmak için
    private Transform currentInteractable; 
    // Not: Bu bir component de olabilir, 
    // örn: private InteractableObject currentInteractable;

    void Start()
    {
        if (fpsCamera == null)
        {
            Debug.LogError("Interactor script'i bir Kamera objesinde olmalı!");
        }
    }

    void Update()
    {
        // 1. ADIM: Nereye baktığımızı her kare kontrol et (Takılma yapmaz)
        CheckForInteractable();

        // 2. ADIM: Etkileşim için GİRİŞ (input) kontrol et
        HandleInteractionInput();
    }

    /// <summary>
    /// Her kare çalışır. Sadece bir şeye bakıp bakmadığımızı kontrol eder.
    /// Ağır bir işlem yapmaz.
    /// </summary>
    private void CheckForInteractable()
    {
        Vector3 rayOrigin = fpsCamera.transform.position;
        Vector3 rayDirection = fpsCamera.transform.forward;
        RaycastHit hitInfo;

        // Raycast'i ateşle
        bool raycastHit = Physics.Raycast(
            rayOrigin,
            rayDirection,
            out hitInfo,
            interactionDistance,
            interactableLayer
        );

        if (raycastHit)
        {
            // Bir şeye çarpıyoruz. Bunu 'mevcut' obje olarak ayarla.
            currentInteractable = hitInfo.transform;
            
            // --- UI GÖSTERME YERİ ---
            // "E Tuşuna Bas" gibi bir yazıyı burada gösterebilirsiniz.
            // Bu kısım her kare çalışacağı için ASLA ağır kod yazmayın
            // (GetComponent, FindObjectOfType vb. burada KULLANILMAMALI)
        }
        else
        {
            // Hiçbir şeye bakmıyoruz.
            currentInteractable = null;

            // --- UI GİZLEME YERİ ---
            // "E Tuşuna Bas" yazısını burada gizleyebilirsiniz.
        }
        
        // Debug için ışını çiz (Sadece editörde)
        Debug.DrawRay(rayOrigin, rayDirection * interactionDistance, raycastHit ? Color.green : Color.red);
    }

    /// <summary>
    /// Her kare çalışır. Sadece 'Sol Tık' yapıldı mı diye bakar.
    /// </summary>
    private void HandleInteractionInput()
    {
        // 'GetKeyDown' sadece tuşa basıldığı *ilk kare* true olur.
        // 'GetKey' kullanırsanız basılı tuttuğunuz sürece çalışır (bunu istemiyoruz).
        if (Input.GetKeyDown(KeyCode.Mouse0)) // Sol Tık (Mouse 0)
        {
            // Tıkladığımız anda 'Interactable' bir şeye bakıyor muyuz?
            if (currentInteractable != null)
            {
                // --- ETKİLEŞİM KODU BURADA ÇALIŞIR ---
                // Bu blok, sadece sol tıka BASTIĞIMIZ AN (1 kare) çalışır.
                // Bütün ağır işlemleri (GetComponent, fonksiyon çağırma vb.)
                // GÜVENLE burada yapabilirsiniz.
                
                Debug.Log(currentInteractable.name + " ile etkileşime girildi! (1 Kez Çalıştı)");

                // Örnek: Çarptığımız objenin "InteractableObject" script'ine ulaşalım
                /*
                InteractableObject objScript = currentInteractable.GetComponent<InteractableObject>();
                if (objScript != null)
                {
                    // O objenin kendi Interact() fonksiyonunu çağır
                    objScript.Interact();
                }
                */
            }
        }
    }
}
