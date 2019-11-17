/// <summary>
/// A platform
/// </summary>
[System.Serializable]
public struct Platform
{
    public PlatformBehaviour platform; // Platform prefab
    public float spawnChance; // Chance to spawn. (this is calculated a bit differently compared to the additions. See WorldCreator for more details)
}