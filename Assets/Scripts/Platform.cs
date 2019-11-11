[System.Serializable]
public struct Platform : IPickRandom
{
    public PlatformBehaviour platform;
    public float spawnChance;
    public float GetChance() => spawnChance;
}