using UnityEngine;

public class PlayerInteraction : MonoBehaviour
{
    [SerializeField] private float interactRange = 2f; // Ne kadar uzağa bakıyoruz
    [SerializeField] private LayerMask interactableLayer; // Interactable layer’ı buraya drag-drop yap
    [SerializeField] private Camera playerCamera; // Genelde main camera olur

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            TryInteract();
        }
    }

    void TryInteract()
    {
        Ray ray = new Ray(playerCamera.transform.position, playerCamera.transform.forward);
        if (Physics.Raycast(ray, out RaycastHit hit, interactRange, interactableLayer))
        {
            Debug.Log("Etkileşilen obje: " + hit.collider.name);

            // Eğer obje özel bir script taşıyorsa oradaki metodu çalıştır
            IInteractable interactable = hit.collider.GetComponent<IInteractable>();
            if (interactable != null)
            {
                interactable.Interact();
            }
        }
    }
}

