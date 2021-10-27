using UnityEngine;

[CreateAssetMenu(fileName = nameof(GameConfigurationData), menuName = "ScriptableObjects/" + nameof(GameConfigurationData), order = 1)]
public class GameConfigurationData : ScriptableObject
{
    [Min(1)] public int setPoints = 5, gameSets = 2;
}