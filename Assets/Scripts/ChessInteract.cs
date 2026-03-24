using UnityEngine;

public class ChessInteract : MonoBehaviour, IInteractable
{
    [Header("Config")]
    [SerializeField] private ChestPasswordSO passwordData;  // Kéo SO vào đây

    private Animator animator;
    private bool isOpen = false;
    private static readonly int IsOpenHash = Animator.StringToHash("isOpening");

    // ─────────────────────────────────────────────
    private void Awake()
    {
        animator = GetComponent<Animator>();
        if (animator == null)
            Debug.LogError($"ChessInteract: Không tìm thấy Animator trên {gameObject.name}");

        if (passwordData == null)
            Debug.LogError($"ChessInteract: Chưa gán PasswordData trên {gameObject.name}");
    }

    private void Start()
    {
        PasswordChessUI.Instance.OnPasswordCorrect += OpenChest;
    }

    private void OnDestroy()
    {
        if (PasswordChessUI.Instance != null)
            PasswordChessUI.Instance.OnPasswordCorrect -= OpenChest;
    }

    // ─────────────────────────────────────────────
    public void Interact()
    {
        if (isOpen) return;
        PasswordChessUI.Instance.Show(passwordData);  // Truyền SO của rương này
    }

    public InteractType GetInteractType()
    {
        return isOpen ? InteractType.Close : InteractType.Open;
    }

    // ─────────────────────────────────────────────
    private void OpenChest()
    {
        isOpen = true;
        animator.SetBool(IsOpenHash, true);
    }
}