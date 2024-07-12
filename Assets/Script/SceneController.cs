using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class SceneController : MonoBehaviour
{
     // Chuyển đến Scene kế tiếp ngay lập tức
    public void LoadNextScene()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentSceneIndex + 1);
    }

    // Chuyển đến Scene kế tiếp với thời gian chờ
    public void LoadNextSceneWithDelay(float delay)
    {
        StartCoroutine(LoadNextSceneAfterDelay(delay));
    }

    private IEnumerator LoadNextSceneAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        LoadNextScene();
    }

    // Chuyển đến Scene theo tên
    public void LoadSceneByName(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }

    // Chuyển đến Scene theo chỉ số
    public void LoadSceneByIndex(int sceneIndex)
    {
        SceneManager.LoadScene(sceneIndex);
    }

    // Khởi động lại Scene hiện tại
    public void RestartScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
