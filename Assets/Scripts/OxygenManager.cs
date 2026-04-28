using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OxygenManager : MonoBehaviour
{
    public static OxygenManager Instance { get; private set; }

    [SerializeField] private float maxOxygen = 150f;
    [SerializeField] private float drainRate = 1f;
    [SerializeField] private ReasonDieSO oxyReason;
    private float currentOxygen;
    private bool isDead = false;
    public event EventHandler<OnOxygenChangedEventArgs> OnOxygenChanged;

     public class OnOxygenChangedEventArgs : EventArgs
    {
        public float oxygenNormalized; // 0 = hết, 1 = đầy
    }


    private void Awake()
    {
        Instance = this;
        currentOxygen = maxOxygen;
    }

    private void Update()
    {
        if (isDead)
        {
            return;
        }
        currentOxygen -= drainRate * Time.deltaTime;
        currentOxygen = Mathf.Clamp(currentOxygen, 0f, maxOxygen);

        OnOxygenChanged?.Invoke(this, new OnOxygenChangedEventArgs
        {
            oxygenNormalized = currentOxygen / maxOxygen
        });

        if(currentOxygen <= 0f)
        {
            isDead = true;
            Debug.Log("Player died due to lack of oxygen.");
            PlayerDieManager.Instance.PlayerDie(oxyReason);
        }
    }

    //public float GetOxygenNormalized()
    //{
    //    return currentOxygen / maxOxygen;
    //}

    public void RefillOxygen(float amount)
    {
        currentOxygen += amount;
        currentOxygen = Mathf.Clamp(currentOxygen, 0f, maxOxygen);
        OnOxygenChanged?.Invoke(this, new OnOxygenChangedEventArgs
        {
            oxygenNormalized = currentOxygen / maxOxygen
        });
    }
}
