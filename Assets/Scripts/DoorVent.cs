using UnityEngine;
using UnityEngine.AI;

public class DoorVent : MonoBehaviour
{
    [SerializeField] private NavMeshObstacle obstacle;
    private Animator doorVentAnimator;
    private bool isOpen;
    private const string ISOPEN = "IsOpenning";

    private void Awake()
    {
        doorVentAnimator = GetComponent<Animator>();
        isOpen = false;

    }

    private void OnEnable()
    {
        LeverInteract.OnLeverPulled += ActiveDoorVent;
    }

    private void OnDisable()
    {
        LeverInteract.OnLeverPulled -= ActiveDoorVent;
    }

    public void ActiveDoorVent(bool isActive)
    {
        doorVentAnimator.SetBool(ISOPEN, isActive);
        isOpen = isActive;

        obstacle.enabled = !isActive;
    }

}   