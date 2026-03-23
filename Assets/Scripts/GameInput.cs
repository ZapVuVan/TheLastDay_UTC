using StarterAssets;
using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.Timeline.Actions;
using UnityEngine;
using UnityEngine.InputSystem;

public class GameInput : MonoBehaviour
{
    public static GameInput Instance { get; private set; }

    public event EventHandler OnInteractAction;
    public event EventHandler OnDropAction;
    public event EventHandler OnQuitNoteAction;

    public event EventHandler<OnSlotChangedEventArgs> OnSlotChanged;
    public class OnSlotChangedEventArgs : EventArgs
    {
        public int slotIndex;
    }

    private PlayerInputActions playerInputActions;
    private InputActionAsset inputActionAsset;

    private void Awake()
    {
        Instance = this;
        playerInputActions = new PlayerInputActions();
        playerInputActions.Player.Enable();
        playerInputActions.Player.Interact.performed += Interact_performed;

        playerInputActions.Player.Drop.performed += ctx
           => OnDropAction?.Invoke(this, EventArgs.Empty);

        playerInputActions.Player.SelectSlot1.performed += ctx
           => OnSlotChanged?.Invoke(this, new OnSlotChangedEventArgs { slotIndex = 0 });
        playerInputActions.Player.SelectSlot2.performed += ctx
            => OnSlotChanged?.Invoke(this, new OnSlotChangedEventArgs { slotIndex = 1 });
        playerInputActions.Player.SelectSlot3.performed += ctx
            => OnSlotChanged?.Invoke(this, new OnSlotChangedEventArgs { slotIndex = 2 });
        playerInputActions.Player.SelectSlot4.performed += ctx
            => OnSlotChanged?.Invoke(this, new OnSlotChangedEventArgs { slotIndex = 3 });
        playerInputActions.Player.SelectSlot5.performed += ctx
            => OnSlotChanged?.Invoke(this, new OnSlotChangedEventArgs { slotIndex = 4 });
        playerInputActions.Note.QuitNote.performed += ctx
            => OnQuitNoteAction?.Invoke(this, EventArgs.Empty);



    }

    private void OnDestroy()
    {
        playerInputActions.Player.Interact.performed -= Interact_performed;
        playerInputActions.Player.Drop.performed -= ctx
           => OnDropAction?.Invoke(this, EventArgs.Empty);
        playerInputActions.Player.Disable();
    }


    private void Interact_performed(InputAction.CallbackContext context)
    {
        OnInteractAction?.Invoke(this, EventArgs.Empty);
    }

    public void DisablePlayerActions()
    {
        playerInputActions.Player.Disable();
        playerInputActions.Note.Enable();
    }

    public void EnablePlayerActions()
    {
        playerInputActions.Player.Enable();
        playerInputActions.Note.Disable();
    }
}




