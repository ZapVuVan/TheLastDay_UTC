using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDieManager : MonoBehaviour
{
    private bool isDie;
    public static PlayerDieManager Instance { get; private set; }

    public event EventHandler<OnPlayerDieEventArgs> OnPlayerDie;

    public class OnPlayerDieEventArgs : EventArgs
    {
        public ReasonDieSO reasonDie;
    }

    private void Awake()
    {
        Instance = this;
    }

    public void PlayerDie(ReasonDieSO reasonDie)
    {
        if(isDie) return;
        isDie = true;
        OnPlayerDie?.Invoke(this, new OnPlayerDieEventArgs { reasonDie = reasonDie });
    }   
}
