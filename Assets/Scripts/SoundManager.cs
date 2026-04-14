using System;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager Instance { get; private set; }
    public event Action<SoundDataSO, Vector3> OnSoundEmitted;

    void Awake()
    {
        if (Instance != null && Instance != this) { Destroy(gameObject); return; }
        Instance = this;
    }

    public void EmitSound(SoundDataSO data, Vector3 position)
    {
        if (data == null) return;
        OnSoundEmitted?.Invoke(data, position);
    }

    // One-shot: cửa, nhặt đồ... vừa báo boss vừa phát audio
    public void EmitSoundWithAudio(SoundDataSO data, Vector3 position)
    {
        if (data == null) return;
        OnSoundEmitted?.Invoke(data, position);
        if (data.clip != null)
            AudioManager.Instance.PlaySFXAt(data.clip, position, data.volume);
    }
}