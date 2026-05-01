
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameMenuUI : MonoBehaviour
{
    [SerializeField] private Button playButton;
    [SerializeField] private Button optionsButton;
    [SerializeField] private Button exitButton;

    [SerializeField] private string gameSceneName = "GamePlay";

    private void Start()
    {
        playButton.onClick.AddListener(OnPlayClicked);
        optionsButton.onClick.AddListener(OnOptionsClicked);
        exitButton.onClick.AddListener(OnExitClicked);
    }

    private void OnPlayClicked()
    {
        LoadingData.sceneToLoad = gameSceneName;
        SceneManager.LoadScene("LoadingScene");
    }

    private void OnOptionsClicked()
    {

        Debug.Log("Options clicked");
    }

    private void OnExitClicked()
    {
        Application.Quit();

    }
}