using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SlotUI : MonoBehaviour
{
    [SerializeField] private Image iconImage;
    [SerializeField] private Image borderImage;
    [SerializeField] private Sprite emptySprite;

    private readonly Color colorNormal = new Color(0f, 0.9f, 1f, 0.25f);
    private readonly Color colorActive = new Color(0f, 0.9f, 1f, 1f);
    private readonly Color iconNormal = new Color(1f, 1f, 1f, 1f);
    private readonly Color iconEmpty = new Color(1f, 1f, 1f, 0.15f);

    public void UpdateSlot(InventorySlot slot, bool isActive)
    {
        if (slot.IsEmpty)
        {
            iconImage.sprite = emptySprite;
            iconImage.color = iconEmpty;
        }
        else
        {
            // L?y icon th?ng t? ItemDataSO — không c?n tìm ki?m gì
            iconImage.sprite = slot.itemData.icon;
            iconImage.color = iconNormal;
        }

        borderImage.color = isActive ? colorActive : colorNormal;
    }
}
