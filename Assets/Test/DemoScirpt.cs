using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DemoScirpt : MonoBehaviour
{
    public InventoryManager inventoryManager;
    public Item[] itemsToPickup;

    public void PickItem(int id)
    {
        bool result = inventoryManager.AddItem(itemsToPickup[id]);
        if(result == true)
        {
            Debug.Log("Item add");
        }
        else
        {
            Debug.Log("Item Not add");
        }
    }
}
