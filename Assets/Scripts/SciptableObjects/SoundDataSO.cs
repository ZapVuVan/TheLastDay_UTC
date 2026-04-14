using UnityEngine;

[CreateAssetMenu(fileName = "SoundData", menuName = "Sound/SoundData")]
public class SoundDataSO : ScriptableObject
{
    [Header("AI Detection")]
    [Range(0f, 1f)] public float noiseValue;

    [Header("Audio")]
    public AudioClip clip;
    [Range(0f, 1f)] public float volume = 1f;
}