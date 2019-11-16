using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; set; }

    public string StartGameButton;
    public float audioFadeTime;
    [HideInInspector] public bool GameStarted = false;

    void Awake()
    {
        if (Instance != null)
        {
            Destroy(this);
            return;
        }
        Instance = this;
    }

    void OnEnable()
    {
        Player.OnPlayerDeath += FadeMusic;
    }

    void OnDisable()
    {
        Player.OnPlayerDeath -= FadeMusic;
    }
    
    void FadeMusic()
    {
        AudioManager.Instance.Fade(audioFadeTime);
    }

    void Update()
    {
        if (Input.GetButtonDown(StartGameButton)) GameStarted = true;
    }
}
