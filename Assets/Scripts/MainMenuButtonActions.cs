using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// Button actions on the main menu
/// </summary>
public class MainMenuButtonActions : MonoBehaviour
{
    // Start the game
    public void Play()
    {
        SceneManager.LoadScene(1);
    }

    // Exit the game
    public void Quit()
    {
        Application.Quit();

        // Little bonus. If we are in the editor, we exit play mode.
#if UNITY_EDITOR
        UnityEditor.EditorApplication.ExitPlaymode();
#endif
    }
}
