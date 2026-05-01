using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LaserPanelUI : MonoBehaviour
{
    public static LaserPanelUI Instance { get; private set; }

    [Header("UI Elements")]
    [SerializeField] private GameObject uiRoot;
    [SerializeField] private Image dotA;
    [SerializeField] private Image dotB;
    [SerializeField] private TextMeshProUGUI labelA;
    [SerializeField] private TextMeshProUGUI labelB;
    [SerializeField] private Button btnA;
    [SerializeField] private Button btnB;

    [Header("Màu text")]
    [SerializeField] private Color onColor = new Color(0.16f, 0.61f, 0.36f);
    [SerializeField] private Color offColor = new Color(0.5f, 0.5f, 0.5f);

    private LaserSwitch _currentSwitch;

    void Awake()
    {
        Instance = this;
        uiRoot.SetActive(false);
    }

    void Start()
    {
        GameInput.Instance.OnQuitNoteAction += OnQuit;
        btnA.onClick.AddListener(OnClickA);
        btnB.onClick.AddListener(OnClickB);
    }

    void OnDestroy()
    {
        GameInput.Instance.OnQuitNoteAction -= OnQuit;
    }

    private void OnQuit(object sender, System.EventArgs e)
    {
        if (uiRoot.activeSelf) Close();
    }

    private void OnClickA()
    {
        if (_currentSwitch == null) return;
        _currentSwitch.SelectA();
        Refresh();
    }

    private void OnClickB()
    {
        if (_currentSwitch == null) return;
        _currentSwitch.SelectB();
        Refresh();
    }

    public void Open(LaserSwitch sw)
    {
        _currentSwitch = sw;
        dotA.color = sw.GetColorA();
        dotB.color = sw.GetColorB();
        uiRoot.SetActive(true);
        Refresh();

        // Giống NoteUI
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        GameInput.Instance.DisablePlayerActions();
    }

    public void Close()
    {
        uiRoot.SetActive(false);
        _currentSwitch = null;

        // Giống NoteUI
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        GameInput.Instance.EnablePlayerActions();
    }

    public bool IsOpen(LaserSwitch sw) => uiRoot.activeSelf && _currentSwitch == sw;

    private void Refresh()
    {
        if (_currentSwitch == null) return;
        bool aOn = _currentSwitch.GetCurrentOn() == _currentSwitch.ColorA;
        bool bOn = _currentSwitch.GetCurrentOn() == _currentSwitch.ColorB;

        labelA.text = aOn ? "ON" : "OFF";
        labelA.color = aOn ? onColor : offColor;
        labelB.text = bOn ? "ON" : "OFF";
        labelB.color = bOn ? onColor : offColor;
    }
}