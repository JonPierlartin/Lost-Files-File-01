using UnityEngine;

public class ItemPickup : MonoBehaviour
{
    [SerializeField] private Item item; // Bu pickup hangi item?

    public Item GetItem() => item;
}
