using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ILevelData
{
    int GetId { get; }
    string GetName { get; }
    int GetLevelIndex { get; }
    int GetLocationLevelIndex { get; }

    int GetLevelCompletedCount { get; }
    int GetIterationIndex { get; }
}
