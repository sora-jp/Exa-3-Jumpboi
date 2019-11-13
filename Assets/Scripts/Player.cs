using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    // ReSharper disable InconsistentNaming
    #pragma warning disable 649 // Field never assigned
    
    [Header("Movement")]
    [SerializeField] float gravity;
    [SerializeField] float jumpSpeed;
    [SerializeField] float xSpeed;
    [SerializeField] float xSpeedSmoothTime;

    [Header("Ground detection")] 
    [SerializeField] float rayHorizDst;
    [SerializeField] LayerMask groundLayer;

    [Header("Graphics")]
    [SerializeField] Sprite jumpingSprite, fallingSprite;
    [SerializeField] new SpriteRenderer renderer;
    [SerializeField] GameObject graphics;
    [SerializeField] ParticleSystem deathParticles;
    
    #pragma warning restore 649
    // ReSharper restore InconsistentNaming

    public static event Action OnPlayerDeath;
    [HideInInspector] public float currentYVel;

    float m_currentXVel;
    float m_targetXVel;
    float m_xVelSmoothingVel;

    bool m_flipX;

    SpriteRenderer m_gfxcopy;
    float m_halfScreenHorizSize;
    Vector3 m_screenOffsetToEdge;

    // Needs to be start, in order to give the camera enough time to update it's size
    void Start()
    {
        // orthographicSize is cameraHeight/2, aspect is cameraWidth/cameraHeight.
        m_halfScreenHorizSize = Camera.main.orthographicSize * Camera.main.aspect;

        // Offset from one edge of the screen to the other
        m_screenOffsetToEdge = Vector3.right * m_halfScreenHorizSize * 2;

        // Clone our graphics object
        m_gfxcopy = Instantiate(graphics, transform).GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        UpdateMovement();
        WrapMovement();
        UpdateGraphics();
        UpdateCopy();
    }

    // I chose to wrap my movement instead of clamping.
    void WrapMovement()
    {
        var pos = transform.position;

        // This wraps the player from one side of the screen to the other
        while (pos.x > m_halfScreenHorizSize) pos.x -= m_halfScreenHorizSize * 2;
        while (pos.x < -m_halfScreenHorizSize) pos.x += m_halfScreenHorizSize * 2;

        // Could have done this instead to clamp
        // pos.x = Mathf.Clamp(pos.x, -m_halfScreenHorizSize, m_halfScreenHorizSize);

        transform.position = pos;
    }

    void UpdateCopy()
    {
        // Position copy offscreen, so that it will be visible if we go off one edge.
        m_gfxcopy.transform.position = transform.position.x > 0
            ? transform.position - m_screenOffsetToEdge
            : transform.position + m_screenOffsetToEdge;

        // Copy our visual parameters to the copy sprite
        m_gfxcopy.flipX = renderer.flipX;
        m_gfxcopy.sprite = renderer.sprite;
    }

    void UpdateGraphics()
    {
        // If our velocity is positive, we display the jumping sprite. otherwise we display the falling sprite.
        renderer.sprite = currentYVel < 0 ? fallingSprite : jumpingSprite;
        
        // Only update the flip variable if we are actually moving on x
        // Otherwise we would flip back to one side, instead of continuing to face to the other side when we let go of the input.
        // Note that we use the target velocity instead of the current velocity. This is because the target velocity is directly based off of the raw input
        if (!Mathf.Approximately(m_targetXVel, 0)) m_flipX = m_targetXVel < 0;

        // Flip the renderer
        renderer.flipX = m_flipX;
    }

    void UpdateMovement()
    {
        // targetXVel is the velocity we want. currentXVel is our actual velocity. We SmoothDamp the current vel to our target in order to make the movement feel smoother.
        m_targetXVel = Input.GetAxisRaw("Horizontal") * xSpeed;

        m_currentXVel = Mathf.SmoothDamp(m_currentXVel, m_targetXVel, ref m_xVelSmoothingVel, xSpeedSmoothTime);
        currentYVel -= gravity * Time.deltaTime;

        // The velocity for this frame.
        var movement = new Vector3(m_currentXVel, currentYVel);

        if (IsGrounded(out var hit)) // Collided with a platform
        {
            // Make sure we always end out movement on the platform, instead of going through it
            movement.y = -hit.distance + 0.5f;

            // Jump
            currentYVel = jumpSpeed;

            // Trigger platform behaviour, to allow e.g. the spring platform to launch us higher.
            var platformBehaviour = hit.transform.GetComponent<PlatformBehaviour>();
            if (platformBehaviour != null) platformBehaviour.OnPlayerCollision(this);
        }

        // Move
        transform.position += movement * Time.deltaTime;
    }

    bool IsGrounded(out RaycastHit2D hit)
    {
        // Needed because out variable
        hit = new RaycastHit2D();

        // We never hit a platform if we are going up
        if (currentYVel >= 0) return false;

        // 0.5 from the player size. 0.5 = half a unit = half of the player size.
        var dst = 0.5f - currentYVel * Time.deltaTime;
        
        // Check left ray first
        hit = Physics2D.Raycast(transform.position - Vector3.right * rayHorizDst / 2, Vector2.down, dst, groundLayer);
        if (hit) return true; // Early out if we hit something (i.e. we are on the ground)

        // Check right ray
        hit = Physics2D.Raycast(transform.position + Vector3.right * rayHorizDst / 2, Vector2.down, dst, groundLayer);
        return hit; // If this hits, we were grounded. Otherwise, we are falling
    }

    // Kills the player, and triggers an event saying that we died (oh no!)
    public void KillPlayer()
    {
        deathParticles.transform.SetParent(null); // Become batman
        deathParticles.Play();
        gameObject.SetActive(false);
        OnPlayerDeath?.Invoke();
    }

    // For debugging, draws a horizontal line to show the distance between the two ground check raycasts
    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawLine(transform.position - Vector3.right * rayHorizDst / 2, transform.position + Vector3.right * rayHorizDst / 2);
    }
}