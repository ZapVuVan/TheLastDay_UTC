using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CellSlotInteract : MonoBehaviour, IInteractable
{
    [SerializeField] private ItemDataSO requiredCell;
    [SerializeField] private GameObject cellVisual;

    private bool isOccupied = false;

    public void Interact()
    {
        if (isOccupied) return; // đã có cell rồi → bỏ qua

        var activeSlot = InventoryManager.Instance.GetActiveSlot();
        if (activeSlot.IsEmpty || activeSlot.itemData != requiredCell) return;

        InventoryManager.Instance.RemoveAndHideActiveItem();
        cellVisual.SetActive(true);
        isOccupied = true;

        // Tắt collider → raycast không hit nữa
        if (TryGetComponent(out Collider col))
            col.enabled = false;

        // Tắt highlight
        if (TryGetComponent(out InteractableHighlight highlight))
            highlight.Unhighlight();

        PowerManager.Instance.InsertCell();
    }


    public InteractType GetInteractType()
        => isOccupied ? InteractType.None : InteractType.Activate;
}
