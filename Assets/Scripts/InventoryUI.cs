using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class InventoryUI : MonoBehaviour
{
    public static InventoryUI Instance { get; private set; }

    [Header("Slots")]
    [SerializeField] private SlotUI[] slotUIs;


    private void Awake()
    {
        if (Instance != null && Instance != this) { Destroy(gameObject); return; }
        Instance = this;
    }

    private void Start()
    {
        InventoryManager.Instance.OnInventoryChanged += (s, e) => RefreshSlots();
        InventoryManager.Instance.OnActiveSlotChanged += (s, e) => RefreshSlots();

        RefreshSlots();
    }

    private void RefreshSlots()
    {
        InventorySlot[] slots = InventoryManager.Instance.GetSlots();
        int activeIndex = InventoryManager.Instance.GetActiveSlotIndex();

        for (int i = 0; i < slotUIs.Length; i++)
            slotUIs[i].UpdateSlot(slots[i], i == activeIndex);
    }
}