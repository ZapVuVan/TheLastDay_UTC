using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NoteUI : MonoBehaviour
{
    [SerializeField] private GameObject notePanel;
    [SerializeField] private Image noteImage;

    public static NoteUI Instance { get; private set; }

    public void Awake()
    {
        Instance = this;
        notePanel.SetActive(false);
    }
    public void Start()
    {
        GameInput.Instance.OnQuitNoteAction += Instance_OnQuitNoteAction;
    }

    private void Instance_OnQuitNoteAction(object sender, System.EventArgs e)
    {
        if (notePanel.activeSelf)
        {
            Hide();
        }
    }

    public void OnDestroy()
    {
        GameInput.Instance.OnQuitNoteAction -= Instance_OnQuitNoteAction;
    }
    public void Update()
    {
        //if (notePanel.activeSelf && GameInput.)
        //{
          
        //}
    }
    public void Show(Sprite sprite)
    {
        noteImage.sprite = sprite;
        notePanel.SetActive(true);
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        GameInput.Instance.DisablePlayerActions();

    }

    public void Hide()
    {
        notePanel.SetActive(false);
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        GameInput.Instance.EnablePlayerActions();
    }
}
