using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonSmokePoisonInteract : MonoBehaviour, IInteractable
{
    private bool isActivated = true;
    private bool isPressed = false;
    public static event  Action<bool> OnButtonPressed;
    public static event Action<bool> OnButtonReleased;
    public void Interact()
    {
        if(isActivated)
        {
            if (!isPressed)
            {
                OnButtonPressed?.Invoke(true);
            }
            else
            {
                OnButtonReleased?.Invoke(true);    
            }
            isPressed = !isPressed;
        }
        else
        {
            OnButtonPressed?.Invoke(false);
        }
    }

    public InteractType GetInteractType() => InteractType.Button;
}
