using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class SimpleImageEffect : MonoBehaviour
{
    public Material effectMat;

    // ReSharper disable once SuggestBaseTypeForParameter, because unity message
    void OnRenderImage(RenderTexture src, RenderTexture dst)
    {
        if (effectMat != null) Graphics.Blit(src, dst, effectMat);
        else Graphics.Blit(src, dst);
    }
}
