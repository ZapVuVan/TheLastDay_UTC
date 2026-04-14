using UnityEngine;

public class SoundEmitter : MonoBehaviour
{
    [Header("Sound Data")]
    public SoundDataSO soundSneak;
    public SoundDataSO soundWalk;
    public SoundDataSO soundRun;

    private AudioSource _loopSource;
    private SoundDataSO _currentData;

    private void Awake()
    {
        _loopSource = gameObject.AddComponent<AudioSource>();
        _loopSource.loop = true;
        _loopSource.spatialBlend = 1f;
    }

    public void HandleFootstep(float currentSpeed, float walkSpeed, float moveSpeed, float sprintSpeed)
    {
        SoundDataSO newData;

        if (currentSpeed < 0.1f)
            newData = null;
        else if (currentSpeed <= walkSpeed + 0.1f)
            newData = soundSneak;
        else if (currentSpeed >= sprintSpeed - 0.1f)
            newData = soundRun;
        else
            newData = soundWalk;

        // Không đổi thì thôi
        if (newData == _currentData) return;
        _currentData = newData;

        if (newData == null || newData.clip == null)
        {
            _loopSource.Stop();
            return;
        }

        _loopSource.clip = newData.clip;
        _loopSource.volume = newData.volume;
        _loopSource.Play();

        if (newData != soundSneak)
            SoundManager.Instance.EmitSound(newData, transform.position);
    }

    // Dùng cho cửa, vật thể...
    public void EmitSound(SoundDataSO data)
        => SoundManager.Instance.EmitSoundWithAudio(data, transform.position);

    public void EmitSoundAt(SoundDataSO data, Vector3 position)
        => SoundManager.Instance.EmitSoundWithAudio(data, position);
}