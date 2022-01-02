using UnityEngine;

[CreateAssetMenu(fileName = "GameConfigs", menuName = MenuPath, order = MenuOrder)]
public class GameConfigs : ScriptableObject, IGameConfigs
{
    private const string MenuPath = "Configs/GameConfigs";
    private const int MenuOrder = int.MinValue + 101;


    [Header("Levels")]
    [SerializeField] private float _levelsChangeDelay = 2f;
    [SerializeField] private float _stagesChangeDelay = 1f;


    public float LevelsChangeDelay => _levelsChangeDelay;
    public float StagesChangeDelay => _stagesChangeDelay;
}
