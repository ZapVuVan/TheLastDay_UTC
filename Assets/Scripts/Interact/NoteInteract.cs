using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoteInteract : MonoBehaviour,IInteractable
{
    [SerializeField] NoteDataSO noteData;
    public void Interact()
    {
        NoteUI.Instance.Show(noteData.noteSprite);
    }

    public InteractType GetInteractType()
    {
        return InteractType.Read;
    }
}
