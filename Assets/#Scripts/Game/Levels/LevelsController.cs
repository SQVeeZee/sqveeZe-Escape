using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class LevelsController : MonoBehaviour,IInitializable, IDisposable
{
    [Inject] protected readonly LevelsConfigs _levelsConfigs = null;

    public ILevelItem GetLevelItem => _currentlevelItem;

    private BaseLevelItem _currentlevelItem = null;
    private int _currentLevelId = 0;

    [Inject] UILevelFailedScreen _levelFailedScreen;
    [Inject] UILevelCompleteScreen _levelCompleteScreen;

    [Inject]
    protected virtual void Initialize(UILevelFailedScreen levelFailedScreen)
    {
        levelFailedScreen.onRestart += StartNextLevel;
    }
    
    [Inject]
    protected virtual void Initialize(UILevelCompleteScreen levelCompleteScreen)
    {
        levelCompleteScreen.onCompleteLevel += StartNextLevel;
    }

    public void Initialize()
    {
        CreateLevel();
    }

    public void Dispose()
    {

    }

    public void StartNextLevel(ELevelCompleteReason levelCompleteReason)
    {
        switch (levelCompleteReason)
        {
            case ELevelCompleteReason.WIN:
                StartNextLevel();
                break;

            case ELevelCompleteReason.LOSE:
                RestartLevel();
                break;
        }
    }

    public void CompleteLevel(ELevelCompleteReason levelCompleteReason)
    {
        switch (levelCompleteReason)
        {
            case ELevelCompleteReason.WIN:
                _levelCompleteScreen.Show();
                break;

            case ELevelCompleteReason.LOSE:
                _levelFailedScreen.Show();
                break;
        }
    }

    private void CreateLevel()
    {
        _currentlevelItem = Instantiate(_levelsConfigs.LevelItemConfigs[_currentLevelId].LevelPrefab);

        _currentlevelItem.onLevelCompleted += CompleteLevel;
    }

    private void RestartLevel()
    {
        DestroyLevel();

        CreateLevel();
    }

    private void DestroyLevel()
    {
        _currentlevelItem.onLevelCompleted -= CompleteLevel;

        Destroy(_currentlevelItem.gameObject);
    }

    private void IncreaseLevelId()
    {
        _currentLevelId++;
    }

    private void StartNextLevel()
    {
        DestroyLevel();

        IncreaseLevelId();

        CreateLevel();
    }
}
