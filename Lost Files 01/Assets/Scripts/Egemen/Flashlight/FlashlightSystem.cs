using UnityEngine;

public class FlashlightSystem : MonoBehaviour
{
    [SerializeField] private PlayerInventory playerInventory;
    [SerializeField] private GameObject lightSource;
    [SerializeField] private string flashlightItemID;

    private bool flashlightOn = false;

    private void Start()
    {
        if (lightSource != null)
            lightSource.SetActive(false);
    }

    private void Update()
    {
        if (playerInventory.HasItem(flashlightItemID) && Input.GetKeyDown(KeyCode.F))
        {
            flashlightOn = !flashlightOn;
            lightSource.SetActive(flashlightOn);
        }
    }
}
