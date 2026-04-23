using System;
using UnityEngine;
using UnityEngine.AI;

public class LeverInteract : MonoBehaviour, IInteractable
{
    public static event Action<bool> OnLeverPulled;

    private bool leverUp = true;
    private Animator leverAnimator;
    public const string LEVER_ACTIVATED = "LeverUp";
    

    private void Awake()
    {
        leverAnimator = GetComponent<Animator>();
    }

    public void Interact()
    {
        if (leverUp)
        {
            leverAnimator.SetBool(LEVER_ACTIVATED, true);
            OnLeverPulled?.Invoke(true);

        }
        else
        {
            leverAnimator.SetBool(LEVER_ACTIVATED, false);
            OnLeverPulled?.Invoke(false);
        }
        leverUp = !leverUp;
    }

    public InteractType GetInteractType() => InteractType.Lever;
}