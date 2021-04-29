using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StarterItemsHelper : MonoBehaviour
{
    public InventoryManager inventoryManager;

    public List<GameItem> ItemsToAdd;

    // Start is called before the first frame update
    void Start()
    {
        foreach (var item in ItemsToAdd)
        {
            Debug.Log(string.Format("Adding item id: {0} type: {1} to inventory", item.ItemID, item.ItemType));
            inventoryManager.AddItem(item);
        }

        
    }

    // Update is called once per frame
    void Update()
    {
        
    }


}
