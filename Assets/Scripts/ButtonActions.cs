using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// Methods that ui buttons call. Could have placed them in the UIManager. Dunno why i didn't, but i can't be bothered to change it now :)
/// </summary>
public class ButtonActions : MonoBehaviour
{
    // Respawn, aka Retry, aka load the current scene again
    public void Respawn()
    {
        // (Re)load the current scene
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    // Quit to main menu
    public void Quit()
    {
        SceneManager.LoadScene(0);
    }
}
