using System.Collections;
using System.Collections.Generic;
using UnityBase.Animations;
using UnityEngine;
using UnityEngine.UI;

public class PlayerDeathAnimationManager : MonoBehaviour
{
    public float duration = 2;
    public DarkenEffect disappearPanel;

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
        UIManager.Instance.NavigateToSubscreen(0);
        disappearPanel.Darken(duration / 2);
    }
}