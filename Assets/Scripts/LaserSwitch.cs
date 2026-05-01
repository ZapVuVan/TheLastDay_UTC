using UnityEngine;

public class LaserSwitch : MonoBehaviour
{
    [SerializeField] private string colorA;
    [SerializeField] private string colorB;

    [Header("Màu hiển thị trên UI")]
    [SerializeField] private Color colorADisplay = Color.red;
    [SerializeField] private Color colorBDisplay = Color.blue;

    private string _currentOn = null;

    public void SelectA() => Select(colorA);
    public void SelectB() => Select(colorB);

    private void Select(string color)
    {
        // Bấm lại màu đang ON → tắt
        if (_currentOn == color)
        {
            LaserColorManager.Instance.TurnOn(color);  // bật laser lại
            _currentOn = null;
            return;
        }

        // Bật lại laser của màu cũ
        if (_currentOn != null)
            LaserColorManager.Instance.TurnOn(_currentOn);

        // Tắt laser màu mới
        LaserColorManager.Instance.TurnOff(color);
        _currentOn = color;
    }

    public string GetCurrentOn() => _currentOn;
    public string ColorA => colorA;
    public string ColorB => colorB;
    public Color GetColorA() => colorADisplay;
    public Color GetColorB() => colorBDisplay;
}