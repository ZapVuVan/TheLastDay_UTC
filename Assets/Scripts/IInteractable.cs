using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum InteractType
{
    None,
    Open,
    Close,
    Hide,
    Refill,
    Pickup,
    Drop,
    Read,
    Activate,
    Craft,
    Vent,
    Radio,
    Lever,
    Button,
    Space,
}
public interface IInteractable
{
    void Interact();
    InteractType GetInteractType();

}


