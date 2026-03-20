using Unity.VisualScripting.Antlr3.Runtime;
using UnityEngine;

public class ChessInteract : MonoBehaviour, IInteractable
{
    private Animator animator;
    private bool isOpen = false;

    private const string OPEN_ANIMATION = "isOpening";
    private void Awake()
    {
        // Tự lấy — không cần kéo tay trong Inspector
        animator = GetComponent<Animator>();

        if (animator == null) { }
            //Debug.LogError($"ChessInteract: Không tìm thấy Animator trên {gameObject.name}");
    }
    public void Interact()
    {
        isOpen = !isOpen;
        animator.SetBool(OPEN_ANIMATION, isOpen);
    }
    public InteractType GetInteractType()
    {
        return isOpen? InteractType.Close: InteractType.Open;

    }
}