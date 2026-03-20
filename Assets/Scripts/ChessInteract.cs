using UnityEngine;

public class ChessInteract : MonoBehaviour, IInteractable
{
    public void Interact()
    {
        Debug.Log("Interact");
    }
    public InteractType GetInteractType()
    {
        return InteractType.Open;
    }
}