using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpringPlatformBehaviour : PlatformBehaviour
{
    public float springMultiplicationFactor;

    public override void OnPlayerCollision(Player player)
    {
        player.currentYVel *= springMultiplicationFactor;
    }
}
