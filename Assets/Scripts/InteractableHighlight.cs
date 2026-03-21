using UnityEngine;

public class InteractableHighlight : MonoBehaviour, IHighlightable
{
    private Outline outline;

    [SerializeField] private Color outlineColor = Color.white;
    [SerializeField] private float outlineWidth = 5f;

    private void Awake()
    {
        outline = gameObject.AddComponent<Outline>();
        outline.OutlineMode = Outline.Mode.OutlineAll;
        outline.OutlineColor = outlineColor;
        outline.OutlineWidth = outlineWidth;
        outline.enabled = false;
    }

    public void Highlight() => outline.enabled = true;
    public void Unhighlight() => outline.enabled = false;
}