using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpaceInteract : MonoBehaviour, IInteractable
{
    


    public void Interact()
    {

    }

    public InteractType GetInteractType() => InteractType.Space;
}
