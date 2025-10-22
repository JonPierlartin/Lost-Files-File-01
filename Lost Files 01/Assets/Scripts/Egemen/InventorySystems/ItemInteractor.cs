using UnityEngine;

public class ItemInteractor : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Camera playerCamera;
    [SerializeField] private PlayerInventory playerInventory;

    [Header("Settings")]
    [SerializeField] private float interactDistance = 3f;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            TryInteract();
        }
    }

    private void TryInteract()
    {
        Ray ray = new Ray(playerCamera.transform.position, playerCamera.transform.forward);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, interactDistance))
        {
            ItemPickup pickup = hit.collider.GetComponent<ItemPickup>();
            if (pickup != null)
            {
                Item item = pickup.GetItem();
                playerInventory.AddItem(item);

                Debug.Log($"{item.itemName} alındı!");
                Destroy(pickup.gameObject);
            }
        }
    }
}
