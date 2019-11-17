using UnityEngine;

public class DiamondOverrideBehaviour : AdditionPositionOverrideBehaviour
{
    public float offsetY;

    public override void PositionOnPlatform(Transform platform)
    {
        var halfScreenWidth = Camera.main.orthographicSize * Camera.main.aspect;

        float x;
        if (platform.position.x < 0)
        {
            x = -(platform.position.x + halfScreenWidth);
        }
        else
        {
            x = halfScreenWidth - platform.position.x;
        }

        x /= 2;

        transform.position = platform.position + Vector3.up * offsetY + Vector3.right * x;
    }
}
