using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DroppingPlatformBehaviour : PlatformBehaviour
{
    public float gravity = 0;

    bool m_drop = false;
    float m_velY = 0;

    void Update()
    {
        if (!m_drop) return;

        m_velY -= gravity * Time.deltaTime;
        transform.position += Vector3.up * m_velY * Time.deltaTime;
    }

    public override void OnPlayerCollision(Player player)
    {
        m_drop = true;
    }
}
