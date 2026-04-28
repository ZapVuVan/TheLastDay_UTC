using UnityEngine;

public class SmokePoison : MonoBehaviour
{
    [SerializeField] private ParticleSystem smokePoison;
    [SerializeField] private GameObject SmokePosition;

    private ParticleSystem _activeSmokeInstance;

    public void OnEnable()
    {
        ButtonSmokePoisonInteract.OnButtonPressed += ActiveSmoke;
        ButtonSmokePoisonInteract.OnButtonReleased += DestroySmoke;
    }

    public void OnDisable()
    {
        ButtonSmokePoisonInteract.OnButtonPressed -= ActiveSmoke;
        ButtonSmokePoisonInteract.OnButtonReleased -= DestroySmoke;
    }

    private void ActiveSmoke(bool obj)
    {
        if (_activeSmokeInstance != null) return;

        _activeSmokeInstance = Instantiate(
            smokePoison,
            SmokePosition.transform.position,
            SmokePosition.transform.rotation
        );
    }

    private void DestroySmoke(bool obj)
    {
        if (_activeSmokeInstance == null) return;

        Destroy(_activeSmokeInstance.gameObject);
        _activeSmokeInstance = null;
    }
}