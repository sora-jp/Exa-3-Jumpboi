using UnityEngine;

/// <summary>
/// Positions the diamond
/// </summary>
public class DiamondOverrideBehaviour : AdditionPositionOverrideBehaviour
{
    public float offsetY;

    // Place the diamond between the platform and the closest edge of the world
    public override void PositionOnPlatform(Transform platform)
    {
        var halfScreenWidth = Camera.main.orthographicSize * Camera.main.aspect;

        float x;

        // Offset to closest edge
        if (platform.position.x < 0)
        {
            x = -(platform.position.x + halfScreenWidth);
        }
        else
        {
            x = halfScreenWidth - platform.position.x;
        }

        // Halve offset
        x /= 2;

        // Position
        transform.position = platform.position + Vector3.up * offsetY + Vector3.right * x;
    }
}
