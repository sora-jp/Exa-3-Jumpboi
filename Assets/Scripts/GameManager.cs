using UnityEngine;
using UnityEngine.Serialization;

/// <summary>
/// Manages 
/// </summary>
public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; set; } // Singleton boilerplate

    [FormerlySerializedAs("StartGameButton")] // Needed because rename
    public string startGameButton; // Input Manager button to start the game.
    public float audioFadeTime; // Duration of audio fade on death
    [HideInInspector] public bool gameStarted;

    void Awake()
    {
        // Singleton boilerplate
        if (Instance != null)
        {
            Destroy(this);
            return;
        }
        Instance = this;
    }

    // Need both OnEnable and OnDisable, otherwise there would be a fucky wucky when we load scenes (too annoying to explain lol)
    void OnEnable()
    {
        Player.OnPlayerDeath += FadeMusic;
    }

    void OnDisable()
    {
        Player.OnPlayerDeath -= FadeMusic;
    }
    
    // Fade to black (Metallica)
    // Fades the music to quiet when we die.
    void FadeMusic()
    {
        AudioManager.Instance.Fade(audioFadeTime);
    }

    void Update()
    {
        // Start the game as soon as we press DA BUTTON
        if (Input.GetButtonDown(startGameButton)) gameStarted = true;
    }
}
