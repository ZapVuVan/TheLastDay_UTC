using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
public class PasswordChessUI : MonoBehaviour
{
    public static PasswordChessUI Instance { get; private set; }

    [Header("References")]
    [SerializeField] private GameObject panel;
    [SerializeField] private TextMeshProUGUI[] digitTexts;
    [SerializeField] private TextMeshProUGUI feedbackText;
    [SerializeField] private Button enterButton;
    [SerializeField] private Button closeButton;

    private string correctPassword;
    private string currentInput = "";

    public event Action OnPasswordCorrect;

    // ─────────────────────────────────────────────
    private void Awake()
    {
        if (Instance != null) { Destroy(gameObject); return; }
        Instance = this;
        panel.SetActive(false);
        enterButton.onClick.AddListener(OnEnterPressed);
        closeButton.onClick.AddListener(Hide);
    }


    public void Show(ChestPasswordSO passwordData)
    {
        correctPassword = passwordData.Password;
        panel.SetActive(true);
        ResetInput();
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        GameInput.Instance.DisablePlayerActions();

        Debug.Log($"Cursor visible: {Cursor.visible}");
        Debug.Log($"Cursor lockState: {Cursor.lockState}");
        Debug.Log($"Panel active: {panel.activeSelf}");
    }

    public void Hide()
    {
        panel.SetActive(false);
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        GameInput.Instance.EnablePlayerActions();
    }

    // ─────────────────────────────────────────────
    public void OnDigitPressed(int digit)
    {
        if (currentInput.Length >= 6) return;

        currentInput += digit.ToString();
        feedbackText.text = "";
        RefreshDigitDisplay();
    }

    public void OnDeletePressed()
    {
        if (currentInput.Length == 0) return;

        currentInput = currentInput[..^1];
        feedbackText.text = "";
        RefreshDigitDisplay();
    }

    private void OnEnterPressed()
    {
        if (currentInput.Length < 6) return;

        if (currentInput == correctPassword)
        {
            OnPasswordCorrect?.Invoke();
            Hide();
        }
        else
        {
            feedbackText.text = "SAI";
            ResetInput();
        }
    }

    // ─────────────────────────────────────────────
    private void RefreshDigitDisplay()
    {
        for (int i = 0; i < digitTexts.Length; i++)
            digitTexts[i].text = i < currentInput.Length ? currentInput[i].ToString() : "_";
    }

    private void ResetInput()
    {
        currentInput = "";
        RefreshDigitDisplay();
    }
}
