using UnityEngine;
using UnityEngine.EventSystems;
using TMPro;
using System.Collections;

public class MenuButtonHover : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
{
    [Header("References")]
    [SerializeField] private TextMeshProUGUI labelText;
    [SerializeField] private TextMeshProUGUI arrowText;

    [Header("Colors")]
    [SerializeField] private Color normalColor = new Color(1f, 1f, 1f, 1f);
    [SerializeField] private Color hoverColor = new Color(0.8f, 0.1f, 0.1f, 1f);

    [Header("Shadow")]
    [SerializeField] private bool useShadow = true;
    [SerializeField] private Color shadowColor = new Color(0.5f, 0f, 0f, 0.6f);
    [SerializeField] private Vector2 shadowOffset = new Vector2(3f, -3f);

    [Header("Animation")]
    [SerializeField] private float hoverScale = 1.05f;
    [SerializeField] private float animSpeed = 8f;

    private Vector3 _originalScale;
    private Coroutine _scaleCoroutine;

    private void Awake()
    {
        _originalScale = transform.localScale;

        // Tự động tìm nếu chưa gán
        if (labelText == null)
            labelText = GetComponentInChildren<TextMeshProUGUI>();

        SetNormal();
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        SetHover();
        ScaleTo(hoverScale);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        SetNormal();
        ScaleTo(1f);
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        ScaleTo(0.95f);
    }

    private void SetHover()
    {
        labelText.color = hoverColor;

        if (useShadow)
        {
            labelText.fontSharedMaterial.EnableKeyword("UNDERLAY_ON");
            labelText.fontSharedMaterial.SetColor("_UnderlayColor", shadowColor);
            labelText.fontSharedMaterial.SetFloat("_UnderlayOffsetX", shadowOffset.x);
            labelText.fontSharedMaterial.SetFloat("_UnderlayOffsetY", shadowOffset.y);
        }

        if (arrowText != null)
            arrowText.gameObject.SetActive(true);
    }

    private void SetNormal()
    {
        labelText.color = normalColor;

        if (useShadow)
            labelText.fontSharedMaterial.DisableKeyword("UNDERLAY_ON");

        if (arrowText != null)
            arrowText.gameObject.SetActive(false);
    }

    private void ScaleTo(float targetScale)
    {
        if (_scaleCoroutine != null) StopCoroutine(_scaleCoroutine);
        _scaleCoroutine = StartCoroutine(ScaleCoroutine(targetScale));
    }

    private IEnumerator ScaleCoroutine(float targetScale)
    {
        Vector3 target = _originalScale * targetScale;
        while (Vector3.Distance(transform.localScale, target) > 0.001f)
        {
            transform.localScale = Vector3.Lerp(
                transform.localScale, target, Time.deltaTime * animSpeed
            );
            yield return null;
        }
        transform.localScale = target;
    }
}