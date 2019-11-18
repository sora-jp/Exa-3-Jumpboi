using UnityEngine;

/// <summary>
/// Used to override the positioning behaviour of an addition.
/// </summary>
public abstract class AdditionPositionOverrideBehaviour : MonoBehaviour
{
    public abstract void PositionOnPlatform(Transform platform);
}
