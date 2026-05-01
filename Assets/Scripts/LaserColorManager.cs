using System.Collections.Generic;
using UnityEngine;

public class LaserColorManager : MonoBehaviour
{
    public static LaserColorManager Instance { get; private set; }

    [System.Serializable]
    public class ColorGroup
    {
        public string colorName;
        public List<GameObject> lasers;
    }

    [SerializeField] private List<ColorGroup> colorGroups;

    private Dictionary<string, List<GameObject>> _map = new();

    void Awake()
    {
        if (Instance != null && Instance != this) { Destroy(gameObject); return; }
        Instance = this;
        foreach (var g in colorGroups)
            _map[g.colorName] = g.lasers;
    }

    public void TurnOff(string colorName)
    {
        if (!_map.ContainsKey(colorName)) return;
        foreach (var obj in _map[colorName])
            if (obj) obj.SetActive(false);
    }

    public void TurnOn(string colorName)
    {
        if (!_map.ContainsKey(colorName)) return;
        foreach (var obj in _map[colorName])
            if (obj) obj.SetActive(true);
    }

    public bool IsOn(string colorName)
    {
        if (!_map.ContainsKey(colorName) || _map[colorName].Count == 0) return false;
        return _map[colorName][0].activeSelf;
    }
}