using UnityBase.Animations;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Placed on the panel that darkens the screen on a game over, manages the material / shader of said panel.
/// </summary>
[RequireComponent(typeof(Image))]
public class DarkenEffect : MonoBehaviour
{
    Material m_disappearMat;
    Image m_image;

    void Awake()
    {
        m_image = GetComponent<Image>();

        // Create a copy of the disappearPanel material and set the panel and our reference to the copy.
        // Otherwise, changes to the material persist outside of play mode, which is not what we want
        m_disappearMat = m_image.material = Instantiate(m_image.material);
    }

    // Darken the panel, gradually making it look more opaque, eventually turning everything behind it black
    public void Darken(float duration)
    {
        this.Animate<float>(val => m_disappearMat.SetFloat("_Transition", val), 0, 1, duration / 2, Mathf.Lerp, EaseMode.Step4);
    }
}
