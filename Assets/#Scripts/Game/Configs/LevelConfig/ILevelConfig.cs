using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ILevelConfig : ILevelData
{
    void SetCompletedCount(int completedCount);
    void SetIterationIndex(int iterationIndex);
    void SetLevelIndex(int levelIndex);
    void SetLocationLevelIndex(int locationLevelIndex);
}
