using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "LevelItem", menuName = MenuPath, order = MenuOrder)]
public class LevelItemConfigs : ScriptableObject, ILevelItemConfigs
{
    private const string MenuPath = "Configs/LevelItemConfigs";
    private const int MenuOrder = int.MinValue + 121;

    [SerializeField] private string _levelName = default;
    [SerializeField] private BaseLevelItem _levelPrefab = null;

    public string LevelName => _levelName;

    public BaseLevelItem LevelPrefab => _levelPrefab;
}
