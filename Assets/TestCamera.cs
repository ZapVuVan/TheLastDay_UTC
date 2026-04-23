using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestCamera : MonoBehaviour
{
    private void Awake()
    {
        Debug.Log(Camera.main.name);
    }
}
