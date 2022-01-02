using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "LevelsConfigs", menuName = MenuPath, order = MenuOrder)]
public class LevelsConfigs : ScriptableObject, ILevelsConfigs
{
    private const string MenuPath = "Configs/LevelsConfigs";
    private const int MenuOrder = int.MinValue + 220;

    [Header("CONFIGS")]
    [SerializeField] private LevelConfig[] _configs = new LevelConfig[0];

    public ILevelConfig GetLevelConfig(int levelIndex)
    {
        ILevelConfig config = null;
        if (levelIndex < _configs.Length)
        {
            config = _configs[levelIndex];
        }
        else
        {
            config = _configs[levelIndex % _configs.Length];
        }

        if (config != null)
        {
            config.SetLocationLevelIndex(levelIndex);
        }

        return config;
    }
}
