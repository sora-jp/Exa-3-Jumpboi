using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float gravity;
    public float jumpSpeed;
    public LayerMask groundLayer;
    public float xSpeed;
    public float xSpeedSmoothTime;

    float m_currentXVel;
    float m_targetXVel;
    float m_xVelSmoothingVel;

    float m_currentYVel;

    void Update()
    {
        m_targetXVel = Input.GetAxisRaw("Horizontal") * xSpeed;

        m_currentXVel = Mathf.SmoothDamp(m_currentXVel, m_targetXVel, ref m_xVelSmoothingVel, xSpeedSmoothTime);
        m_currentYVel -= gravity * Time.deltaTime;

        var movement = new Vector3(m_currentXVel, m_currentYVel);

        if (m_currentYVel < 0)
        {
            var dst = 0.5f - m_currentYVel * Time.deltaTime;
            var hit = Physics2D.Raycast(transform.position, Vector2.down, dst, groundLayer);
            if (hit)
            {
                movement.y = -hit.distance + 0.5f;
                m_currentYVel = jumpSpeed;
            }
        }

        transform.position += movement * Time.deltaTime;
    }
}
