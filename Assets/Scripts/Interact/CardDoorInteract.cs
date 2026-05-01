
using UnityEngine;

public class CardDoorInteract : MonoBehaviour, IInteractable
{
    [Header("Card yêu cầu")]
    [SerializeField] private ItemDataSO requiredCard;

    [Header("Animation")]
    [SerializeField] private Animator doorAnimator;
    [SerializeField] private string open = "IsOpening";

    private bool isOpen = false;

    public void Interact()
    {
        if (isOpen) return;

        // Kiểm tra slot active đang cầm đúng card không
        InventorySlot activeSlot = InventoryManager.Instance.GetActiveSlot();
        if (activeSlot.IsEmpty || activeSlot.itemData != requiredCard)
        {
            Debug.Log("[Door] Cần card để mở cửa!");
            return;
        }

        // Mở cửa
        isOpen = true;
        if (doorAnimator != null)
            doorAnimator.SetBool(open, true);

        Debug.Log("[Door] Cửa đã mở!");
    }

    public InteractType GetInteractType() => InteractType.None;
}