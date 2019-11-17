using UnityEngine;

public class SpringPlatformBehaviour : PlatformBehaviour
{
    public float springMultiplicationFactor;
    public string springTriggerName;
    public Animator springAnimator;

    public override void OnPlayerCollision(Player player)
    {
        player.currentYVel *= springMultiplicationFactor;
        springAnimator.SetTrigger(springTriggerName);
    }
}
