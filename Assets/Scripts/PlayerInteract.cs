using StarterAssets;
using System;
using UnityEngine;

public class PlayerInteract : MonoBehaviour
{
    public static PlayerInteract Instance { get; private set; }

    [SerializeField] private Camera playerCamera;
    [SerializeField] private float interactDistance = 2f;
    [SerializeField] private LayerMask interactLayerMask;

    private IInteractable currentInteractable;
     
    public event EventHandler OnInteractableChanged;

    public event EventHandler<OnSelectedInteractableChangedEventArgs> OnSelectedInteractableChanged;
    public class OnSelectedInteractableChangedEventArgs : EventArgs
    {
        public IInteractable selectedInteractable;
    }

    private void Awake()
    {
        if (Instance != null && Instance != this) { Debug.LogError($"Duplicate PlayerInteract! Destroying {gameObject.name}");  Destroy(gameObject); return; }
        Instance = this;
    }

    private void Start()
    {
        GameInput.Instance.OnInteractAction += GameInput_OnInteractAction;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    private void OnDestroy()
    {
        GameInput.Instance.OnInteractAction -= GameInput_OnInteractAction;
    }

    private void GameInput_OnInteractAction(object sender, EventArgs e)
    {
        Debug.Log($"Interact fired! currentInteractable: {currentInteractable}");
        if (currentInteractable != null)
            currentInteractable.Interact();
    }

    private void Update()
    {
        HandleInteractCheck();
    }

    private void HandleInteractCheck()
    {
        Ray ray = playerCamera.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));
        IInteractable newInteractable = null;

        if (Physics.Raycast(ray, out RaycastHit hit, interactDistance, interactLayerMask))
            if (hit.collider.TryGetComponent(out IInteractable interactable))
                newInteractable = interactable;

        if (newInteractable != currentInteractable)
        {
            currentInteractable = newInteractable;

            // Fire cả 2 event
            OnInteractableChanged?.Invoke(this, EventArgs.Empty);
            OnSelectedInteractableChanged?.Invoke(this,
                new OnSelectedInteractableChangedEventArgs
                {
                    selectedInteractable = currentInteractable
                });
        }
    }

    public bool HasInteractable() => currentInteractable != null;
    public IInteractable GetCurrentInteractable() => currentInteractable;
}