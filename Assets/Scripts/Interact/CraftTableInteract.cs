using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CraftTableInteract : MonoBehaviour, IInteractable
{
    CraftingTable craftingTable;

    void Awake()
    {
        craftingTable = GetComponent<CraftingTable>();
    }
    public void Interact()
    {
        craftingTable.Craft();
    }
    public InteractType GetInteractType() => InteractType.Craft;
}
