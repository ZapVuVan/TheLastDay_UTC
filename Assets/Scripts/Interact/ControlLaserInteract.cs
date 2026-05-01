using UnityEngine;

public class ControlLaserInteract : MonoBehaviour, IInteractable
{
    [SerializeField] private LaserSwitch laserSwitch;

    public InteractType GetInteractType() => InteractType.None;

    public void Interact()
    {
        if (LaserPanelUI.Instance.IsOpen(laserSwitch))
            LaserPanelUI.Instance.Close();
        else
            LaserPanelUI.Instance.Open(laserSwitch);
    }
}