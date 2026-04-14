using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RefillInteract : MonoBehaviour,IInteractable
{
    private OxygenBarUI oxygenBarUI;
    [SerializeField] private float refillAmount = 200f;
    public void Interact()
    {
        OxygenManager.Instance.RefillOxygen(refillAmount);
    }
    public InteractType GetInteractType()
    {
        return InteractType.Refill;
    }

}
