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
}
public interface IInteractable
{
    void Interact();
    InteractType GetInteractType();

}


