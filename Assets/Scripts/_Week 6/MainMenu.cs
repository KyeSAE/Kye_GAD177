using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    [Header("Scene")]
    public string firstSceneName = "Main Game";

    // Hook this function up to the Play UI Button.
    public void LoadScene()
    {
        // Load the scene stored in firstSceneName.
        SceneManager.LoadScene(firstSceneName);

    }

    // Hook this function up to the Quit UI Button.
    public void QuitGame()
    {
        // Quit the application.
        Application.Quit();
    }
}