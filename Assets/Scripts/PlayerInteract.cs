using StarterAssets;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteract : MonoBehaviour
{
    [SerializeField] private Camera playerCamera;
    [SerializeField] private float interactDistance = 2f;
    [SerializeField] private LayerMask interactLayerMask;

    private StarterAssetsInputs starterAssetsInputs;
    private IInteractable currentInteractable;

    private void Start()
    {
        GameInput.Instance.OnInteractAction += GameInput_OnInteractAction;
    }

    private void OnDestroy()
    {
        GameInput.Instance.OnInteractAction -= GameInput_OnInteractAction;
    }

    private void GameInput_OnInteractAction(object sender, EventArgs e)
    {
        if (currentInteractable != null)
        {
            currentInteractable.Interact();
        }
    }


    private void Awake()
    {
        starterAssetsInputs = GetComponent<StarterAssetsInputs>();

    }

    private void Update()
    {
        HandleInteractCheck();
    }

    private void HandleInteractCheck()
    {
        Ray ray = playerCamera.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));

        if (Physics.Raycast(ray, out RaycastHit hit, interactDistance, interactLayerMask))
        {
            // Ch? update khi nhìn vào object khác
            if (hit.collider.TryGetComponent(out IInteractable interactable))
            {
                if (interactable != currentInteractable)
                    SetCurrentInteractable(interactable);
            }
            else
            {
                SetCurrentInteractable(null);
            }
        }
        else
        {
            SetCurrentInteractable(null);
        }
    }

    private void SetCurrentInteractable(IInteractable interactable)
    {
        currentInteractable = interactable;
    }


}
