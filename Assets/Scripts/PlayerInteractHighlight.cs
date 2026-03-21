using System;
using UnityEngine;

public class PlayerInteractHighlight : MonoBehaviour
{
    private IHighlightable currentHighlighted;

    private void Start()
    {
        PlayerInteract.Instance.OnSelectedInteractableChanged
            += PlayerInteract_OnSelectedInteractableChanged;
    }

    private void OnDestroy()
    {
        PlayerInteract.Instance.OnSelectedInteractableChanged
            -= PlayerInteract_OnSelectedInteractableChanged;
    }

    private void PlayerInteract_OnSelectedInteractableChanged(object sender,
        PlayerInteract.OnSelectedInteractableChangedEventArgs e)
    {
        // Tắt highlight cũ
        currentHighlighted?.Unhighlight();

        // Bật highlight mới
        if (e.selectedInteractable != null)
        {
            var go = (e.selectedInteractable as MonoBehaviour)?.gameObject;
            currentHighlighted = go?.GetComponent<IHighlightable>();
            currentHighlighted?.Highlight();
        }
        else
        {
            currentHighlighted = null;
        }
    }
}
