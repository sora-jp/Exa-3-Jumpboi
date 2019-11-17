using System.Collections;
using UnityBase.Animations;
using UnityEngine;

/// <summary>
/// Fades the world and shows the death screen when the player dies
/// </summary>
public class PlayerDeathAnimationManager : MonoBehaviour
{
    public float duration = 2; // Total animation duration
    public CanvasGroup gameOverPanel;
    public DarkenEffect disappearPanel; // The panel that causes the world to disappear

    void Awake()
    {
        gameOverPanel.gameObject.SetActive(false);
        gameOverPanel.alpha = 0;
    }

    // Need both OnEnable and OnDisable, otherwise there would be a fucky wucky when we load scenes (too annoying to explain lol)
    void OnEnable()
    {
        Player.OnPlayerDeath += ShowDeathScreen;
    }

    void OnDisable()
    {
        Player.OnPlayerDeath -= ShowDeathScreen;
    }

    void ShowDeathScreen()
    {
        // Start the animation
        StartCoroutine(_ShowDeathScreen());
    }

    IEnumerator _ShowDeathScreen()
    {
        disappearPanel.Darken(duration / 2);
        yield return new WaitForSeconds(duration / 2);
        gameOverPanel.gameObject.SetActive(true);
        gameOverPanel.AnimateAlpha(1, duration / 2, EaseMode.Step4);
    }
}