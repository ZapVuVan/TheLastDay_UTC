using UnityEngine;

public class BossSound : MonoBehaviour
{
    [SerializeField] private AudioClip roarClip;
    [SerializeField] private AudioClip footstepClip;
    [SerializeField] private AudioClip chaseClip;
    [SerializeField] private AudioClip detectClip;
    public void PlayRoar()
    {
        AudioManager.Instance.PlaySFXAt(roarClip, transform.position, 1f);
    }

    public void PlayFootstep()
    {
        AudioManager.Instance.PlaySFXAt(footstepClip, transform.position, 1f);
    }
    public void PlayChase()
    {
        AudioManager.Instance.PlaySFXAt(chaseClip, transform.position, 1f);
    }
    public void PlayDetect()
    {
        AudioManager.Instance.PlaySFXAt(detectClip, transform.position, 1f);
    }

}