using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu()]
public class ItemDataSO : ScriptableObject
{
    public Sprite icon;
    public GameObject prefab;

    [Header("Hold settings")]
    public Vector3 holdPosition = new Vector3(0.3f, -0.3f, 0.5f);
    public Vector3 holdRotation = new Vector3(15f, -30f, 10f);
}
