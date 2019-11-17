using UnityEngine;

public class ObstacleAdditionBehaviour : AdditionBehaviour
{
    public float offsetY;
    public float xRange;

    protected override void OnPlayerCollision(Player player)
    {
        
    }

    public override void PositionOnPlatform(Transform platform)
    {
        var x = Mathf.Round(Random.Range(-xRange, xRange) * 16) / 16;

        transform.position = Vector3.up * (offsetY + platform.position.y) + Vector3.right * x;
    }
}
