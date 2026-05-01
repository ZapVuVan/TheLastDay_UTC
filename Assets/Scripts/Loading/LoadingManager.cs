
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class LoadingManager : MonoBehaviour
{
    [Header("UI")]
    [SerializeField] private TextMeshProUGUI progressText;  // Text hi?n th? %
    [SerializeField] private TextMeshProUGUI tipText;       // Text tips

    private string[] tips = {
        "Tips: Hãy khám phá từng góc ngách về trạm vũ trụ nhé, bạn sẽ tìm ra những thứ hay ho đó...",
        "Tips: Cẩn thận kẻ thù phía sau!",
        "Tips: Thu thập đủ nguyên liệu để cất cánh.",
        "Tips: Đừng quên kiểm tra inventory thường xuyên."
    };

    private void Start()
    {
        tipText.text = tips[Random.Range(0, tips.Length)];

        string sceneName = LoadingData.sceneToLoad;
        if (string.IsNullOrEmpty(sceneName))
        {
            Debug.LogError("[Loading] Không có scene nào ???c ch? ??nh!");
            return;
        }

        StartCoroutine(LoadSceneAsync(sceneName));
    }

    private IEnumerator LoadSceneAsync(string sceneName)
    {
        AsyncOperation operation = SceneManager.LoadSceneAsync(sceneName);
        operation.allowSceneActivation = false;

        float displayProgress = 0f;

        while (!operation.isDone)
        {
            float targetProgress = Mathf.Clamp01(operation.progress / 0.9f);

            // S? t?ng m??t d?n v? target th?c
            displayProgress = Mathf.MoveTowards(displayProgress, targetProgress, Time.deltaTime * 0.5f);
            progressText.text = $"{(int)(displayProgress * 100)}%";

            if (operation.progress >= 0.9f)
            {
                // ??m n?t lên 100%
                while (displayProgress < 1f)
                {
                    displayProgress = Mathf.MoveTowards(displayProgress, 1f, Time.deltaTime * 0.5f);
                    progressText.text = $"{(int)(displayProgress * 100)}%";
                    yield return null;
                }

                progressText.text = "100%";
                yield return new WaitForSeconds(0.8f);
                operation.allowSceneActivation = true;
            }

            yield return null;
        }
    }
}