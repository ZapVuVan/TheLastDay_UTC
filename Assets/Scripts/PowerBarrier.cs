using UnityEngine;

public class PowerBarrier : MonoBehaviour
{
    [SerializeField] private Animator animator;
    private static readonly int IsOpenHash = Animator.StringToHash("isOpen");

    private void Start()
    {
        PowerManager.Instance.OnPowerActivated += OnPowerActivated;
    }

    private void OnDestroy()
    {
        PowerManager.Instance.OnPowerActivated -= OnPowerActivated;
    }

    private void OnPowerActivated(object sender, System.EventArgs e)
    {
        animator.SetBool(IsOpenHash, true);
    }
}
