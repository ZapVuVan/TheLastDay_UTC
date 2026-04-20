using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VentInteract : MonoBehaviour, IInteractable
{
    [SerializeField] private GameObject poisonBall;

    private void Awake()
    {
        poisonBall.SetActive(false);
    }
    public void Interact()
    {
        poisonBall.SetActive(true);
    }
    public InteractType GetInteractType()
    {
        return InteractType.Vent;
    }

}
