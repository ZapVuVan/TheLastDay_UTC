using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OxygenManager : MonoBehaviour
{
    public static OxygenManager Instance { get; private set; }

    [SerializeField] private float maxOxygen = 150f;
    [SerializeField] private float drainRate = 1f;

    private float currentOxygen;

    public event EventHandler<OnOxygenChangedEventArgs> OnOxygenChanged;

     public class OnOxygenChangedEventArgs : EventArgs
    {
        public float oxygenNormalized; // 0 = hết, 1 = đầy
    }

    public event EventHandler OnOxygenEmpty;

    private void Awake()
    {
        Instance = this;
        currentOxygen = maxOxygen;
    }

    private void Update()
    {
        currentOxygen -= drainRate * Time.deltaTime;
        currentOxygen = Mathf.Clamp(currentOxygen, 0f, maxOxygen);

        OnOxygenChanged?.Invoke(this, new OnOxygenChangedEventArgs
        {
            oxygenNormalized = currentOxygen / maxOxygen
        });

        if(currentOxygen < 0f)
        {
            OnOxygenEmpty?.Invoke(this, EventArgs.Empty);
        }
    }

    public float GetOxygenNormalized()
    {
        return currentOxygen / maxOxygen;
    }

}
