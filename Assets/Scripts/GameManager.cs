using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; set; }

    public string StartGameButton;
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

    void Update()
    {
        if (Input.GetButtonDown(StartGameButton)) GameStarted = true;
    }
}
