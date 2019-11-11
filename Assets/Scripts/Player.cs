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

    public Sprite jumpingSprite, fallingSprite;
    public new SpriteRenderer renderer;
    public GameObject graphics;
    
    public float currentYVel;

    float m_currentXVel;
    float m_targetXVel;
    float m_xVelSmoothingVel;

    bool m_flipX;

    SpriteRenderer m_gfxcopy;
    float m_halfScreenHorizSize;
    Vector3 m_screenOffsetToEdge;

    void Awake()
    {
        m_halfScreenHorizSize = Camera.main.orthographicSize * Camera.main.aspect;
        m_screenOffsetToEdge = Vector3.right * m_halfScreenHorizSize * 2;
        m_gfxcopy = Instantiate(graphics, transform).GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        UpdateMovement();
        ClampMovement();
        UpdateGraphics();
        UpdateCopy();
    }

    void ClampMovement()
    {
        var pos = transform.position;
        while (pos.x > m_halfScreenHorizSize) pos.x -= m_halfScreenHorizSize * 2;
        while (pos.x < -m_halfScreenHorizSize) pos.x += m_halfScreenHorizSize * 2;
        transform.position = pos;
    }

    void UpdateCopy()
    {
        m_gfxcopy.transform.position = transform.position.x > 0
            ? transform.position - m_screenOffsetToEdge
            : transform.position + m_screenOffsetToEdge;

        m_gfxcopy.flipX = renderer.flipX;
        m_gfxcopy.sprite = renderer.sprite;
    }

    void UpdateGraphics()
    {
        renderer.sprite = currentYVel < 0 ? fallingSprite : jumpingSprite;
        if (!Mathf.Approximately(m_targetXVel, 0)) m_flipX = m_targetXVel < 0;
        renderer.flipX = m_flipX;
    }

    void UpdateMovement()
    {
        m_targetXVel = Input.GetAxisRaw("Horizontal") * xSpeed;

        m_currentXVel = Mathf.SmoothDamp(m_currentXVel, m_targetXVel, ref m_xVelSmoothingVel, xSpeedSmoothTime);
        currentYVel -= gravity * Time.deltaTime;

        var movement = new Vector3(m_currentXVel, currentYVel);

        if (IsGrounded(out var hit))
        {
            movement.y = -hit.distance + 0.5f;
            currentYVel = jumpSpeed;

            var platformBehaviour = hit.transform.GetComponent<PlatformBehaviour>();
            if (platformBehaviour != null) platformBehaviour.OnPlayerCollision(this);
        }

        transform.position += movement * Time.deltaTime;
    }

    bool IsGrounded(out RaycastHit2D hit)
    {
        hit = new RaycastHit2D();
        if (currentYVel >= 0) return false;
        
        var dst = 0.5f - currentYVel * Time.deltaTime;
        hit = Physics2D.Raycast(transform.position, Vector2.down, dst, groundLayer);
        
        return hit;
    }
}