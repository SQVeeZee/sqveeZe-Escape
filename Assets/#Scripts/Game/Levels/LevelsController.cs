using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class LevelsController : MonoBehaviour
{
    public event Action<int, ILevelConfig> onLevelConfigSet = null;
    public event Action<int, ILevelItem> onLevelItemSet = null;

    [SerializeField] protected Transform _rootTransform = null;

    protected int _currentLevelIndex = 0;
    protected ILevelConfig _currentLevelConfig = null;
    protected ILevelItem _currentLevelItem = null;
    protected GameObject _currentLevelObject = null;

    protected int _currentLevelLocationIndex = 0;
    protected int _currentLocationIndex = 0;
    protected GameObject _currentLocationObject = null;

    public int GetCurrentLevelIndex => _currentLevelIndex;
    public ILevelConfig GetCurrentLevelConfig => _currentLevelConfig;
    public ILevelItem GetCurrentLevelItem => _currentLevelItem;

    public int GetCurrentLevelLocationIndex => _currentLevelLocationIndex;
    public int GetCurrentLocationIndex => _currentLocationIndex;

    public void InstantiateCurrentLevel()
    {

    }


}
