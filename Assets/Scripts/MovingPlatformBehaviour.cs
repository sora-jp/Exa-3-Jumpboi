using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatformBehaviour : PlatformBehaviour
{
    public float moveRangeX; // The range that this platform can move on the x axis.
    public float sidePadding; // Padding from the level sides.
    public float unitsPerSec; // Movement speed

    float m_minX, m_maxX; // Minimum and maximum x positions
    float m_dst; // Distance between min and max x
    float m_curPos; // The current logical position along the line from minX to maxX
    float m_xPos; // The actual position in unity coords, which goes back and forth.

    void Start()
    {
        // Calculations galore.
        var halfScreenWidth = Camera.main.orthographicSize * Camera.main.aspect;
        var rightSideX = halfScreenWidth - sidePadding;

        // TODO: Describe
        m_minX = Mathf.Clamp(transform.position.x - moveRangeX / 2, -rightSideX, rightSideX);
        m_maxX = Mathf.Clamp(transform.position.x + moveRangeX / 2, -rightSideX, rightSideX);

        m_dst = Mathf.Abs(m_maxX - m_minX); // Distance between minx and maxx
        m_curPos = m_dst / 2; // Start in the middle of the range.
    }

    void Update()
    {
        m_curPos += Time.deltaTime * unitsPerSec;
        m_xPos = Mathf.Lerp(m_minX, m_maxX, Mathf.PingPong(m_curPos / m_dst, 1)); // Ping pong makes the value go pong ping. (Look at docs boi)

        // Pixel align with the mathf round thingy. Basically makes the position only go in increments of 1/16 (1 unity unit = 1 sprite = 16 px)
        transform.position = new Vector3(Mathf.Round(m_xPos * 16) / 16, transform.position.y);
    }

    // Empty override, needed because abstract lol
    public override void OnPlayerCollision(Player player) { }
}
