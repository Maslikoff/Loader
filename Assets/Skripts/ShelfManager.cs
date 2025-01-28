using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShelfManager : MonoBehaviour
{
    public Transform[] shelfSlots;
    public GameObject[] currentItems;

    private void Start()
    {
        StartItemsPosition();
    }

    public bool IsSlotFree(int slotIndex)
    {
        if(slotIndex < 0 || slotIndex >= shelfSlots.Length)
            return false;

        return currentItems[slotIndex] == null;
    }

    public void PlaceItemInSlot(GameObject item, int slotIndex)
    {
        if (IsSlotFree(slotIndex))
        {
            item.transform.position = shelfSlots[slotIndex].position;
            item.transform.rotation = shelfSlots[slotIndex].rotation;
            currentItems[slotIndex] = item;
        }
        else
        {
            Debug.LogWarning("The slots are full!");
        }
    }

    public void RemoveItemFromSlot(int slotIndex)
    {
        if(slotIndex >= 0 && slotIndex < currentItems.Length)
            currentItems[slotIndex] = null;
    }

    private void StartItemsPosition()
    {
        for (int i = 0; i < currentItems.Length; i++)
            currentItems[i].transform.position = shelfSlots[i].position;
    }
}
