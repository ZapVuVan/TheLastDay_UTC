using UnityEngine;

public class ChessInteract : MonoBehaviour, IInteractable
{
    [Header("De trong neu khong can mat khau")]
    [SerializeField] private ChestPasswordSO passwordData;  

    private Animator animator;
    private bool isOpen = false;
    private static readonly int IsOpenHash = Animator.StringToHash("isOpening");

    // ─────────────────────────────────────────────
    private void Awake()
    {
        animator = GetComponent<Animator>();
      
    }

    private void Start()
    {
        if (passwordData != null)
            PasswordChessUI.Instance.OnPasswordCorrect += OpenChest;
    }

    private void OnDestroy()
    {
        if(passwordData != null)
            PasswordChessUI.Instance.OnPasswordCorrect -= OpenChest;
    }

    // ─────────────────────────────────────────────
    public void Interact()
    {
        if (isOpen) return;
        
        if(passwordData != null)
            PasswordChessUI.Instance.Show(passwordData);
        else
            OpenChest();
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
        if (TryGetComponent(out Collider col))
            col.enabled = false;
    }
}