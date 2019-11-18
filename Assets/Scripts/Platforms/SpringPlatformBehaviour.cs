using UnityEngine;

/// <summary>
/// Bouncy boi platform, makes player jump higher
/// </summary>
public class SpringPlatformBehaviour : PlatformBehaviour
{
    public float springMultiplicationFactor;
    public string springTriggerName;
    public Animator springAnimator;

    public override void OnPlayerCollision(Player player)
    {
        player.currentYVel *= springMultiplicationFactor; // Player vel will always be = to jumpForce, because that is set right before this is called
        springAnimator.SetTrigger(springTriggerName); // Make the spring go bouncy bounce
    }
}
