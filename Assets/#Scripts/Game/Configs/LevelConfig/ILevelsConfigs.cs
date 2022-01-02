using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ILevelsConfigs
{
    ILevelConfig GetLevelConfig(int levelIndex);
}
