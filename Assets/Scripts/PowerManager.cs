using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerManager : MonoBehaviour
{
    public static PowerManager Instance { get; private set; }

    [SerializeField] private int requiredCells = 2;
    private int insertedCells = 0;

    public event EventHandler OnCellInserted;
    public event EventHandler OnPowerActivated;

    private void Awake()
    {
        if (Instance != null && Instance != this) { Destroy(gameObject); return; }
        Instance = this;
    }

    public void InsertCell()
    {
        insertedCells++;
        OnCellInserted?.Invoke(this, EventArgs.Empty);
        if (insertedCells >= requiredCells)
            OnPowerActivated?.Invoke(this, EventArgs.Empty);
    }
}
