using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleAdditionBehaviour : AdditionBehaviour
{
    public float offsetY;
    public float xRange;

    protected override void OnPlayerCollision(Player player)
    {
        
    }

    public override void PositionOnPlatform(Transform platform)
    {
        var m_halfScreenWidth = Camera.main.orthographicSize * Camera.main.aspect;

        float x = Mathf.Round(Random.Range(-xRange, xRange) * 16) / 16;
        //if (platform.position.x > 0)
        //{
        //    x = -(platform.position.x + m_halfScreenWidth);
        //}
        //else
        //{
        //    x = m_halfScreenWidth - platform.position.x;
        //}

        //x /= 2;



        transform.position = Vector3.up * (offsetY + platform.position.y) + Vector3.right * x;
    }
}
