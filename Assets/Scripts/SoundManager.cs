using System;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager Instance { get; private set; }

    public event Action<float, Vector3> OnSoundEmitted;

    void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
    }

    public void EmitSound(float noiseValue, Vector3 position)
    {
        OnSoundEmitted?.Invoke(noiseValue, position);
    }
}