using UnityEngine;

public class PickUpInteract : MonoBehaviour, IInteractable
{
    [SerializeField] private ItemDataSO itemData;

    public void Interact()
    {
        // Tắt physics + collider — việc của item
        if (TryGetComponent(out Rigidbody rb))
        {
            rb.isKinematic = true;
            rb.useGravity = false;
        }
        if (TryGetComponent(out Collider col))
            col.enabled = false;

        // Thêm vào inventory — việc của manager
        bool success = InventoryManager.Instance.AddItem(itemData, gameObject);

        if (success)
            // Lo việc visual — việc của ItemHolder
            ItemHolder.Instance.HoldItem(itemData, gameObject);
        else
        {
            // Thất bại — bật lại physics
            if (TryGetComponent(out Rigidbody rb2)) rb2.isKinematic = false;
            if (TryGetComponent(out Collider col2)) col2.enabled = true;
        }
    }

    public InteractType GetInteractType() => InteractType.Pickup;
}