using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropInteract : MonoBehaviour
{
    public void Interact()
    {
        Debug.Log("PickUpInteract: Interact called");
        // Implement the logic for picking up the item here
    }

    public InteractType GetInteractType()
    {
        return InteractType.Drop;
    }
}
