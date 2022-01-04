using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ILevelItemConfigs
{
    string LevelName { get; }
    BaseLevelItem LevelPrefab { get; }
}
