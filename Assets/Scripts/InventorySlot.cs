using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventorySlot
{
    public ItemDataSO itemData;
    public GameObject itemObject;
    public bool IsEmpty => itemData == null;
    public void Clear()
    {
        itemData = null;
        itemObject = null;
    }
}