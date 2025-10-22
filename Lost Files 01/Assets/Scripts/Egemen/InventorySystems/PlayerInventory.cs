using UnityEngine;
using System.Collections.Generic;

public class PlayerInventory : MonoBehaviour
{
    public List<string> ownedItemIDs = new List<string>();

    public void AddItem(Item item)
    {
        if (!ownedItemIDs.Contains(item.itemID))
        {
            ownedItemIDs.Add(item.itemID);
            Debug.Log(item.itemName + " envantere eklendi.");
        }
    }

    public bool HasItem(string id)
    {
        return ownedItemIDs.Contains(id);
    }
}
