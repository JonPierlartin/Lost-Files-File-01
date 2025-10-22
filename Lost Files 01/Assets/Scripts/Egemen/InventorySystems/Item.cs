using UnityEngine;

    [CreateAssetMenu(fileName = "NewItem", menuName = "Inventory/Item")]
    public class Item : ScriptableObject
    {
        public string itemName;
        public string itemID;  // her item için benzersiz ID
    }

