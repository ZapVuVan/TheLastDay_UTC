using System;
using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    public static InventoryManager Instance { get; private set; }

    [SerializeField] private int slotCount = 5;

    private InventorySlot[] slots;
    private int activeSlotIndex = 0;

    public event EventHandler OnInventoryChanged;
    public event EventHandler OnActiveSlotChanged;

    private void Awake()
    {
        if (Instance != null && Instance != this) { Destroy(gameObject); return; }
        Instance = this;

        slots = new InventorySlot[slotCount];
        for (int i = 0; i < slotCount; i++)
            slots[i] = new InventorySlot();
    }

    private void Start()
    {
        GameInput.Instance.OnSlotChanged += GameInput_OnSlotChanged;
        OnActiveSlotChanged?.Invoke(this, EventArgs.Empty);
    }

    private void OnDestroy()
    {
        GameInput.Instance.OnSlotChanged -= GameInput_OnSlotChanged;
    }

    private void GameInput_OnSlotChanged(object sender, GameInput.OnSlotChangedEventArgs e)
        => SetActiveSlot(e.slotIndex);

    // Chỉ lưu data — không đụng visual
    public bool AddItem(ItemDataSO data, GameObject itemObject)
    {
        // Slot active trống → dùng luôn
        if (slots[activeSlotIndex].IsEmpty)
        {
            slots[activeSlotIndex].itemData = data;
            slots[activeSlotIndex].itemObject = itemObject;
            OnInventoryChanged?.Invoke(this, EventArgs.Empty);
            OnActiveSlotChanged?.Invoke(this, EventArgs.Empty);
            return true;
        }

        // Tìm slot trống khác
        for (int i = 0; i < slots.Length; i++)
        {
            if (slots[i].IsEmpty)
            {
                slots[i].itemData = data;
                slots[i].itemObject = itemObject;
                activeSlotIndex = i;
                OnInventoryChanged?.Invoke(this, EventArgs.Empty);
                OnActiveSlotChanged?.Invoke(this, EventArgs.Empty);
                return true;
            }
        }

        return false;
    }

    public bool RemoveActiveItem()
    {
        if (slots[activeSlotIndex].IsEmpty) return false;

        // Chỉ clear data — KHÔNG đụng vào itemObject
        // ItemHolder tự lo việc drop/destroy
        slots[activeSlotIndex].Clear();
        OnInventoryChanged?.Invoke(this, EventArgs.Empty);
        OnActiveSlotChanged?.Invoke(this, EventArgs.Empty);
        return true;
    }

    public bool RemoveAndHideActiveItem()
    {
        if (slots[activeSlotIndex].IsEmpty) return false;

        GameObject itemObject = slots[activeSlotIndex].itemObject;
        if (itemObject != null)
        {
            itemObject.transform.SetParent(null);
            itemObject.SetActive(false); // ẩn thay vì destroy
        }

        slots[activeSlotIndex].Clear();
        OnInventoryChanged?.Invoke(this, EventArgs.Empty);
        OnActiveSlotChanged?.Invoke(this, EventArgs.Empty);
        return true;
    }

    public void SetActiveSlot(int index)
    {
        if (index < 0 || index >= slots.Length) return;
        if (index == activeSlotIndex) return;

        activeSlotIndex = index;
        OnActiveSlotChanged?.Invoke(this, EventArgs.Empty);
    }

    public bool HasItem(ItemDataSO data)
    {
        foreach (var slot in slots)
            if (slot.itemData == data) return true;
        return false;
    }

    public int FindSlot(ItemDataSO data)
    {
        for (int i = 0; i < slots.Length; i++)
            if (slots[i].itemData == data) return i;
        return -1;
    }

    public InventorySlot[] GetSlots() => slots;
    public InventorySlot GetActiveSlot() => slots[activeSlotIndex];
    public int GetActiveSlotIndex() => activeSlotIndex;

    public bool IsFull()
    {
        foreach (var slot in slots)
            if (slot.IsEmpty) return false;
        return true;
    }
}