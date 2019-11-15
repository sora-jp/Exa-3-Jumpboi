using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiamondOverrideBehaviour : AdditionPositionOverrideBehaviour
{
    public float offsetY;

    public override void PositionOnPlatform(Transform platform)
    {
        var m_halfScreenWidth = Camera.main.orthographicSize * Camera.main.aspect;

        float x = 0;
        if (platform.position.x < 0)
        {
            x = -(platform.position.x + m_halfScreenWidth);
        }
        else
        {
            x = m_halfScreenWidth - platform.position.x;
        }

        x /= 2;

        transform.position = platform.position + Vector3.up * offsetY + Vector3.right * x;
    }
}
