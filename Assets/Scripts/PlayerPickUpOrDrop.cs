using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPickUpOrDrop : MonoBehaviour
{
    private PickUpInteract pickUpInteract;
    private DropInteract dropInteract;

    public void Update()
    {
        if (pickUpInteract != null)
        {
            pickUpInteract.Interact();
            pickUpInteract = null; // Reset after interaction
        }
        else if (dropInteract != null)
        {
            dropInteract.Interact();
            dropInteract = null; // Reset after interaction
        }
    }
}

