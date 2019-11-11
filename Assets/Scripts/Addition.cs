[System.Serializable]
public struct Addition : IPickRandom
{
    public AdditionBehaviour addition;
    public float spawnChance;
    public float GetChance() => spawnChance;
}