
/// <summary>
/// Represents an addition, i.e. an object that can be randomly spawned around or on a platform. 
/// </summary>
[System.Serializable]
public struct Addition
{
    public AdditionBehaviour addition; // Addition prefab
    public float spawnChance; // Percent chance to spawn (0..1)
}