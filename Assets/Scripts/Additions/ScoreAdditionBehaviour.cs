using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreAdditionBehaviour : AdditionBehaviour
{
    public int scoreToAdd;
    public ParticleSystem onCollectedParticlesPrefab;

    protected override void OnPlayerCollision(Player player)
    {
        //TODO: Score.
        // Destroy for now
        Destroy(gameObject);
    }

    public override void PositionOnPlatform(Transform platform)
    {
        transform.position = platform.position + Vector3.up * (1 + 2f/16); // 18 pixels, 1 unit is 16 px
    }
}
