using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DemoScirpt : MonoBehaviour
{
    public InventoryManager inventoryManager;
    public ItemSO[] itemsToPickup;

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

    public void GetSelectedItem()
    {
        ItemSO receivedItem = inventoryManager.GetSelectedItem(false);

        if(receivedItem != null)
        {
            Debug.Log("Received item");
        }
        else
        {
            Debug.Log("Received Not item");
        }
    }

    public void UseSelectedItem()
    {
        ItemSO receivedItem = inventoryManager.GetSelectedItem(true);

        if (receivedItem != null)
        {
            Debug.Log("use item");
        }
        else
        {
            Debug.Log("use Not item");
        }
    }
}
