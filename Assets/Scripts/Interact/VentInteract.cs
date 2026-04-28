using UnityEngine;

public class VentInteract : MonoBehaviour, IInteractable
{
    [SerializeField] private GameObject poisonBall;
    [SerializeField] private ItemDataSO poisonBallData;

    private void Awake()
    {
        poisonBall.SetActive(false);
    }

    public void Interact()
    {
        // Kiểm tra xem active slot có phải PoisonBall không
        InventorySlot activeSlot = InventoryManager.Instance.GetActiveSlot();

        if (activeSlot.itemData == poisonBallData)
        {
            // Xóa item khỏi inventory (không drop, không destroy)
            InventoryManager.Instance.RemoveAndHideActiveItem();

            // Bật poisonBall trên vent
            poisonBall.SetActive(true);
        }
        // Nếu không cầm PoisonBall → không làm gì (hoặc thêm feedback)
    }

    public InteractType GetInteractType()
    {
        return InteractType.Vent;
    }
}