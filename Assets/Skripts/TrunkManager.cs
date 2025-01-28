using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrunkManager : MonoBehaviour
{
    public Transform[] trunkSlots; 

    private GameObject[] _currentItems;

    void Start()
    {
        _currentItems = new GameObject[trunkSlots.Length]; 
    }

    public bool TryAddItem(GameObject item)
    {
        for (int i = 0; i < trunkSlots.Length; i++)
        {
            if (_currentItems[i] == null) 
            {
                _currentItems[i] = item;
                item.transform.position = trunkSlots[i].position;
                item.transform.rotation = trunkSlots[i].rotation;

                return true; 
            }
        }

        Debug.LogWarning("The trunk is full!");
        return false; 
    }
}
