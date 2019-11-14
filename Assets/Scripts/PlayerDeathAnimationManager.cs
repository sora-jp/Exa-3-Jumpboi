using System.Collections;
using System.Collections.Generic;
using UnityBase.Animations;
using UnityEngine;
using UnityEngine.UI;

public class PlayerDeathAnimationManager : MonoBehaviour
{
    public float duration = 2;
    public CanvasGroup deathScreen;
    public Image disappearPanel;

    Material m_disappearMat;

    void Awake()
    {
        // Create a copy of the disappearPanel material and set the panel and our reference to the copy.
        // Otherwise, changes to the material persist outside of playmode, which is not what we want
        m_disappearMat = disappearPanel.material = Instantiate(disappearPanel.material);
    }

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
        //StartCoroutine(_AnimateDeathScreen());
        this.Animate<float>(val => m_disappearMat.SetFloat("_Transition", val), 0, 1, duration / 2, Mathf.Lerp, EaseMode.Step4);
        //yield return new WaitForSeconds(duration / 2);
        UIManager.Instance.NavigateToSubscreen(0);
    }

    //IEnumerator _AnimateDeathScreen()
    //{
    //}
}