using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OxygenBarUI : MonoBehaviour
{
    [SerializeField] private Image barFill;

    private readonly Color colorFull = new Color(0f, 0.9f, 1f, 1f); // cyan
    private readonly Color colorMedium = new Color(1f, 0.84f, 0f, 1f); // vàng
    private readonly Color colorLow = new Color(1f, 0.09f, 0.26f, 1f); // ??

    private void Start()
    {
        // L?ng nghe event — ?úng ki?u CodeMonkey
        OxygenManager.Instance.OnOxygenChanged += OxygenManager_OnOxygenChanged;

        // Set giá tr? ban ??u
        UpdateBar(1f);
    }

    private void OnDestroy()
    {
        OxygenManager.Instance.OnOxygenChanged -= OxygenManager_OnOxygenChanged;
    }

    private void OxygenManager_OnOxygenChanged(object sender,
        OxygenManager.OnOxygenChangedEventArgs e)
    {
        UpdateBar(e.oxygenNormalized);
    }

    private void UpdateBar(float normalizedValue)
    {
        // C?p nh?t fillAmount
        barFill.fillAmount = normalizedValue;

        // ??i màu theo ng??ng
        if (normalizedValue > 0.5f)
            barFill.color = colorFull;
        else if (normalizedValue > 0.25f)
            barFill.color = colorMedium;
        else
            barFill.color = colorLow;
    }
}
