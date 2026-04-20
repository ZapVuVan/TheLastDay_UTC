using UnityEngine;

public class Radio : MonoBehaviour
{
    [SerializeField] private SoundDataSO radioSoundData;
    [SerializeField] private AudioClip radioClip;

    private AudioSource _audioSource;
    private bool _isPlaying = false;

    void Awake()
    {
        _audioSource = gameObject.AddComponent<AudioSource>();
        _audioSource.clip = radioClip;
        _audioSource.loop = false;
        _audioSource.spatialBlend = 1f;
    }

    public void Toggle()
    {
        if (_isPlaying) return; // Đang phát thì không làm gì

        _isPlaying = true;
        _audioSource.Play();

        // Báo boss có tiếng động
        SoundManager.Instance.EmitSound(radioSoundData, transform.position);

        // Khi clip hết thì cho phép bật lại
        Invoke(nameof(OnFinished), radioClip.length);
    }

    void OnFinished()
    {
        _isPlaying = false;
    }
}