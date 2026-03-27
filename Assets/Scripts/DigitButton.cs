using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

[RequireComponent(typeof(Button))]
public class DigitButton : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
{
    [SerializeField] private int digit;

    private Button button;
    private Image image;

    private readonly Color normalColor = new Color(1f, 1f, 1f, 1f);
    private readonly Color hoverColor = new Color(0.8f, 0.8f, 0.8f, 1f);
    private readonly Color clickColor = new Color(0.6f, 0.6f, 0.6f, 1f);

    private void Start()
    {
        button = GetComponent<Button>();
        image = GetComponent<Image>();

        button.onClick.AddListener(() =>
            PasswordChessUI.Instance.OnDigitPressed(digit));
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        Debug.Log($"Hover vào button {digit}");  // ← thêm
        image.color = hoverColor;   // đổi màu khi hover

    }

    public void OnPointerExit(PointerEventData eventData)
    {
        image.color = normalColor;  // về bình thường khi rời
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        Debug.Log($"Click button {digit}");
        image.color = clickColor;   // flash tối khi click
        Invoke(nameof(ResetColor), 0.1f);
    }

    private void ResetColor() => image.color = normalColor;
}