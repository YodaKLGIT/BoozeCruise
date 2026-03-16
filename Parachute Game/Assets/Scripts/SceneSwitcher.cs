using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneSwitcher : MonoBehaviour
{
    public void LoadScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
        Debug.Log("Loading scene: " + sceneName);
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
