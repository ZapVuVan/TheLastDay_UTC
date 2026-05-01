
using TMPro;
using UnityEngine;

public class DieUI : MonoBehaviour
{
    private void Start()
    {
        Hide();
        PlayerDieManager.Instance.OnPlayerDie += PlayerDieManager_OnPlayerDie;

    }

    private void PlayerDieManager_OnPlayerDie(object sender, PlayerDieManager.OnPlayerDieEventArgs e)
    {
        Debug.Log("Die reason: " + e.reasonDie);
        reasonDieTMP.text = e.reasonDie.reason;
        Show();
    }

    [SerializeField] private TextMeshProUGUI reasonDieTMP;


    private void Hide()
    {
        gameObject.SetActive(false);
    }

    private void Show()
    {
        gameObject.SetActive(true);
    }
}
