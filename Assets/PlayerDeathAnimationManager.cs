using System.Collections;
using System.Collections.Generic;
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

        // Make sure that the deathScreen is invisible / not interactable
        deathScreen.blocksRaycasts = false;
        deathScreen.interactable = false;
        SetAnimationProgress(0); 
        deathScreen.gameObject.SetActive(false);
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
        // Make sure that you can interact / click on the death screen
        deathScreen.gameObject.SetActive(true);
        deathScreen.blocksRaycasts = true;
        deathScreen.interactable = true;

        // Start the animation
        StartCoroutine(_AnimateDeathScreen());
    }

    IEnumerator _AnimateDeathScreen()
    {
        float t = 0;
        while (t < 1)
        {
            SetAnimationProgress(t);
            t += Time.deltaTime / duration;
            yield return null;
        }
        SetAnimationProgress(1);
    }

    void SetAnimationProgress(float t)
    {
        m_disappearMat.SetFloat("_Transition", t * 4f/3);
        disappearPanel.SetMaterialDirty(); // Dunno if needed, but better safe than sorry!

        deathScreen.alpha = Mathf.Max(Mathf.Round(t * 4 * 4f/3) / 4 - 1f/3, 0);
    }
}
