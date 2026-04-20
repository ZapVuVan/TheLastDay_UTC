using UnityEngine;

public class RadioInteract : MonoBehaviour, IInteractable
{
    private Radio _radio;

    void Awake()
    {
        _radio = GetComponent<Radio>();
    }

    public void Interact()
    {
        _radio.Toggle();
    }

    public InteractType GetInteractType()
    {
        return InteractType.Radio;
    }
}