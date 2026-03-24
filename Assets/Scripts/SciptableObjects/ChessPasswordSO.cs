using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu()]
public class ChestPasswordSO : ScriptableObject
{
    [field: SerializeField] public string Password { get; private set; } = "000000";
}
