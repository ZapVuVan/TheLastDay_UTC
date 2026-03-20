using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlayerInteractUI : MonoBehaviour
{
    [SerializeField] private GameObject _interactUI;
    [SerializeField] private PlayerInteract playerInteract;
    [SerializeField] private TextMeshProUGUI interactText;

    private void Start()
    {
        playerInteract.OnInteractableChanged += PlayerInteract_OnInteractableChanged;
        Hide();
    }
    private void OnDestroy()
    {
        playerInteract.OnInteractableChanged -= PlayerInteract_OnInteractableChanged;
    }
    private void PlayerInteract_OnInteractableChanged(object sender, EventArgs e)
    {
        if (playerInteract.HasInteractable())
        {
            interactText.text = GetTextFromType(playerInteract.GetCurrentInteractable().GetInteractType());
            Show();
        }
        else
            Hide();
    }

    private string GetTextFromType(InteractType type)
    {
        switch (type)
        {
            case InteractType.Open: return "Mở";
            case InteractType.Close: return "Đóng";
            case InteractType.Hide: return "Trốn vào";  
            case InteractType.Refill: return "Refill Oxy";
            case InteractType.Pickup: return "Nhặt";
            case InteractType.Drop: return "Bỏ";
            case InteractType.Activate: return "Kích hoạt";
            default: return "Tương tác";
        }
    }


    //private void Update()
    //{
    //    if(playerInteract.HandleInteractCheck() != null)
    //    {
    //        Show();
    //    }
    //    else
    //    {
    //        Hide();
    //    }
    //}
    private void Show()
    {
        _interactUI.SetActive(true);
    }

    private void Hide()
    {
        _interactUI.SetActive(false);
    }
}
