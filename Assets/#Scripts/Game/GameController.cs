using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class GameController : MonoBehaviour
{
    [Inject] LevelsController _levelsController;
    [Inject] SlowMoController _slowMoController;
    [Inject] UIGameScreen _gameScreen;

    [Inject]
    protected virtual void Initialize(LevelsController levelsController)
    {
        levelsController.onLevelDestroy += OnLevelDestroy;
        levelsController.onLevelCreate += OnLevelCreate;
        levelsController.onLevelCompleted += OnLevelCompleted;
    }

    private void Awake()
    {
        _levelsController.CreateLevel();
    }

    private void OnLevelDestroy(BaseLevelItem currentlevelItem)
    {
        currentlevelItem.onSlowMoActive -= _slowMoController.StartSlowMo;
        currentlevelItem.onPathCompleted -= OnPathCompleted;
    }

    private void OnLevelCompleted()
    {
        _slowMoController.ResetSlowMo();
    }

    private void OnPathCompleted(Action callback)
    {
        _slowMoController.ResetSlowMo(callback);
        _gameScreen.Hide(true);
    }

    private void OnLevelCreate(BaseLevelItem _currentlevelItem)
    {
        _currentlevelItem.onSlowMoActive += _slowMoController.StartSlowMo;
        _currentlevelItem.onPathCompleted += OnPathCompleted;
    
        _gameScreen.Show();
    }
}
