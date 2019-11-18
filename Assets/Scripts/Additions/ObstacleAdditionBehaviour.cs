using UnityEngine;

/// <summary>
/// Addition that kills the player on contact (handled by other script, this just positions)
/// </summary>
public class ObstacleAdditionBehaviour : AdditionBehaviour
{
    public float offsetY;
    public float xRange;

    protected override void OnPlayerCollision(Player player)
    {
        // Needed because abstract
    }

    // Position it randomly on x, and with a fixed offset on y
    public override void PositionOnPlatform(Transform platform)
    {
        // Pixel align x coordinate.
        var x = Mathf.Round(Random.Range(-xRange, xRange) * 16) / 16;

        // Position
        transform.position = Vector3.up * (offsetY + platform.position.y) + Vector3.right * x;
    }
}
