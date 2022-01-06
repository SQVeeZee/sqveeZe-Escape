using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ILevelItem
{
    event Action<Action> onPathCompleted;

    event Action<ELevelCompleteReason> onLevelCompleted;

    public event Action onSlowMoActive;
}
