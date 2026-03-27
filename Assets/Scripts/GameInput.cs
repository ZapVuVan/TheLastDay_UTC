using StarterAssets;
using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class GameInput : MonoBehaviour
{
    public static GameInput Instance { get; private set; }

    [SerializeField] private float mouseSensitivity = 0.1f;

    public event EventHandler OnInteractAction;
    public event EventHandler OnDropAction;
    public event EventHandler OnQuitNoteAction;
    public event EventHandler<OnSlotChangedEventArgs> OnSlotChanged;
    public class OnSlotChangedEventArgs : EventArgs
    {
        public int slotIndex;
    }

    // Expose cho FirstPersonController
    public Vector2 move => playerInputActions.Player.Move.ReadValue<Vector2>();
    public Vector2 look => playerInputActions.Player.Look.ReadValue<Vector2>() * mouseSensitivity;
    public bool sprint => playerInputActions.Player.Sprint.IsPressed();
    public bool jump
    {
        get => _jump;
        set => _jump = value;
    }
    public bool analogMovement = false;

    private bool _jump;
    private PlayerInputActions playerInputActions;

    // ─────────────────────────────────────────────
    private void Awake()
    {
        Instance = this;
        playerInputActions = new PlayerInputActions();
        playerInputActions.Player.Enable();

        playerInputActions.Player.Interact.performed += Interact_performed;
        playerInputActions.Player.Drop.performed += Drop_performed;
        playerInputActions.Player.Jump.performed += ctx => _jump = true;
        playerInputActions.Player.Jump.canceled += ctx => _jump = false;

        playerInputActions.Player.SelectSlot1.performed += ctx => TriggerSlot(0);
        playerInputActions.Player.SelectSlot2.performed += ctx => TriggerSlot(1);
        playerInputActions.Player.SelectSlot3.performed += ctx => TriggerSlot(2);
        playerInputActions.Player.SelectSlot4.performed += ctx => TriggerSlot(3);
        playerInputActions.Player.SelectSlot5.performed += ctx => TriggerSlot(4);

        playerInputActions.UI.QuitNote.performed += ctx =>
            OnQuitNoteAction?.Invoke(this, EventArgs.Empty);
    }

    private void OnDestroy()
    {
        playerInputActions.Player.Interact.performed -= Interact_performed;
        playerInputActions.Player.Drop.performed -= Drop_performed;
        playerInputActions.Dispose();
    }

    // ─────────────────────────────────────────────
    private void Interact_performed(InputAction.CallbackContext ctx)
        => OnInteractAction?.Invoke(this, EventArgs.Empty);

    private void Drop_performed(InputAction.CallbackContext ctx)
        => OnDropAction?.Invoke(this, EventArgs.Empty);

    private void TriggerSlot(int index)
        => OnSlotChanged?.Invoke(this, new OnSlotChangedEventArgs { slotIndex = index });

    // ─────────────────────────────────────────────
    public void DisablePlayerActions()
    {
        playerInputActions.Player.Disable();
        playerInputActions.UI.Enable();
    }

    public void EnablePlayerActions()
    {
        playerInputActions.Player.Enable();
        playerInputActions.UI.Disable();
    }
}