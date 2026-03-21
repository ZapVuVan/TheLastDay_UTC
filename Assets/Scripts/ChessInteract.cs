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
        Debug.Log("Chess Interact called!"); // có log này không?
        isOpen = !isOpen;
        Debug.Log($"animator: {animator}"); // null không?
        animator.SetBool(OPEN_ANIMATION, isOpen);
    }
    public InteractType GetInteractType()
    {
        return isOpen? InteractType.Close: InteractType.Open;

    }
}