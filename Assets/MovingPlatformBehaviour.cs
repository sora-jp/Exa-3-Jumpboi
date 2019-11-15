using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatformBehaviour : PlatformBehaviour
{
    public float moveRangeX;
    public float sidePadding;
    public float unitsPerSec;

    float m_minX, m_maxX;
    float m_dst;
    float m_curPos, m_xPos;

    void Start()
    {
        var halfScreenWidth = Camera.main.orthographicSize * Camera.main.aspect;
        m_minX = Mathf.Clamp(transform.position.x - moveRangeX / 2, -halfScreenWidth + sidePadding, halfScreenWidth - sidePadding);
        m_maxX = Mathf.Clamp(transform.position.x + moveRangeX / 2, -halfScreenWidth + sidePadding, halfScreenWidth - sidePadding);
        m_dst = Mathf.Abs(m_maxX - m_minX);
        m_curPos = m_dst / 2;
    }

    void Update()
    {
        m_curPos += Time.deltaTime * unitsPerSec;
        m_xPos = Mathf.Lerp(m_minX, m_maxX, Mathf.PingPong(m_curPos / m_dst, 1));
        transform.position = new Vector3(Mathf.Round(m_xPos * 16) / 16, transform.position.y);
    }

    public override void OnPlayerCollision(Player player)
    {
        
    }
}
