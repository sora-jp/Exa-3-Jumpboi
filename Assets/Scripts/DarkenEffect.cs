using System.Collections;
using System.Collections.Generic;
using UnityBase.Animations;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Image))]
public class DarkenEffect : MonoBehaviour
{
    Material m_disappearMat;
    Image m_image;

    void Awake()
    {
        m_image = GetComponent<Image>();

        // Create a copy of the disappearPanel material and set the panel and our reference to the copy.
        // Otherwise, changes to the material persist outside of playmode, which is not what we want
        m_disappearMat = m_image.material = Instantiate(m_image.material);
    }

    public void Darken(float duration)
    {
        SetVisibility(true, duration);
    }

    public void Lighten(float duration)
    {
        SetVisibility(false, duration);
    }

    void SetVisibility(bool darken, float duration)
    {
        // Ask me about this, too annoying to explain in a comment.
        // But the 0 represents completely see-through while 1 is completely black.
        if (duration <= 0) m_disappearMat.SetFloat("_Transition", darken ? 1 : 0);
        else this.Animate<float>(val => m_disappearMat.SetFloat("_Transition", val), darken ? 0 : 1, darken ? 1 : 0, duration / 2, Mathf.Lerp, EaseMode.Step4);
    }
}
