using System;
using UnityEngine;

public class ItemHolder : MonoBehaviour
{
    public static ItemHolder Instance { get; private set; }

    [SerializeField] private Transform grabPoint;

    private void Awake()
    {
        if (Instance != null && Instance != this) { Destroy(gameObject); return; }
        Instance = this;
    }

    private void Start()
    {
        InventoryManager.Instance.OnActiveSlotChanged += OnActiveSlotChanged;
        GameInput.Instance.OnDropAction += GameInput_OnDropAction;
    }

    private void GameInput_OnDropAction(object sender, EventArgs e)
    {
        DropActiveItem();
    }

    private void OnActiveSlotChanged(object sender, System.EventArgs e)
    {
        // Ẩn hết
        foreach (var slot in InventoryManager.Instance.GetSlots())
            slot.itemObject?.SetActive(false);

        // Hiện slot active
        var active = InventoryManager.Instance.GetActiveSlot();
        active.itemObject?.SetActive(true);
    }

    public void HoldItem(ItemDataSO data, GameObject itemObject)
    {
        itemObject.transform.SetParent(grabPoint);
        itemObject.transform.localPosition = data.holdPosition;
        itemObject.transform.localRotation = Quaternion.Euler(data.holdRotation);
    }

    private void DropActiveItem()
    {
        var activeSlot = InventoryManager.Instance.GetActiveSlot();
        if (activeSlot.IsEmpty) return;

        GameObject itemObject = activeSlot.itemObject;

        // Tách khỏi GrabPoint
        itemObject.transform.SetParent(null);

        // Reset scale về ban đầu
        itemObject.transform.localScale = Vector3.one;

        // Bật lại physics → rơi xuống đất
        if (itemObject.TryGetComponent(out Rigidbody rb))
        {
            rb.isKinematic = false;
            rb.useGravity = true;
            // Văng về phía trước
            rb.AddForce(grabPoint.forward * 3f, ForceMode.Impulse);
        }

        // Bật lại collider → có thể nhặt lại
        if (itemObject.TryGetComponent(out Collider col))
            col.enabled = true;

        // Xóa khỏi inventory
        InventoryManager.Instance.RemoveActiveItem();
    }
}