using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum InteractType
{
    Open,
    Hide,
    Refill,
    Pickup,
    Activate,
}
public interface IInteractable
{
    void Interact();
    InteractType GetInteractType();

}


