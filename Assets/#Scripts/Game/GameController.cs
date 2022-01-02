using System;
using System.Collections;
using UnityEngine;
using Zenject;

public class GameController : MonoBehaviour, IInitializable, IDisposable
{
    // [Inject] protected LevelsConfigs _levelsConfigs = null;

    [Inject] protected readonly LevelsController _levelsController = null;

    /*[Inject] protected readonly UILevelCompleteScreen _levelCompleteScreen = null;
    [Inject] protected readonly UILevelFailedScreen _levelFailedScreen = null;
    [Inject] protected readonly UILevelInfoScreen _levelInfoScreen = null;
*/
    [Inject] protected readonly MonoBehaviourController _monoBehaviourController = null;

    /*[Inject]*/ protected readonly TimeScaleController _timeScaleController = null;
    /*[Inject]*/ protected readonly SlowMoManager _slowMoManager = null;

    public bool IsLoading { get; private set; }

    protected bool _isStopTimeScale = false;

    [Inject]
    protected virtual void Initialize(MonoBehaviourController monoBehaviourController)
    {
        monoBehaviourController.onLateStart += OnLateStart;
    }

    /*[Inject]
    protected virtual void Initialize(LevelsController levelsController)
    {
        levelsController.onLevelConfigSet += OnLevelConfigSet;
        levelsController.onLevelItemSet += OnLevelItemSet;
    }*/

    /*[Inject]
    protected virtual void Initialize(IPlayerCharacterController playerCharacterController)
    {
        playerCharacterController.onDead += OnPlayerCharacterDied;
    }*/

    /*[Inject]
    protected virtual void Initialize(UILevelCompleteScreen levelCompleteScreen)
    {
        // levelCompleteScreen.onRestartButtonClick += OnLevelRestartButtonClick;
        levelCompleteScreen.onContinueButtonClick += OnLevelCompleteButtonClick;
    }*/

   /* [Inject]
    protected virtual void Initialize(UILevelFailedScreen levelFailedScreen)
    {
        levelFailedScreen.onRestart += OnLevelRestart;
        levelFailedScreen.onContinue += OnLevelContinue;
    }*/

    public virtual void Initialize()
    {

    }

    public void Dispose()
    {
        if (_monoBehaviourController != null)
        {
            _monoBehaviourController.onLateStart -= OnLateStart; ;
        }

        PlayTimeScale();
    }

    protected virtual void Awake()
    {

    }

    protected virtual void OnLateStart()
    {

    }

    protected virtual void OnDestroy()
    {
    }


    protected virtual void OnLevelRestart()
    {
        PlayTimeScale();
    }

    #region TIMESCALE

    protected virtual void PlayTimeScale()
    {
        if (_slowMoManager != null)
        {
            _slowMoManager.UnpauseSlowMo();
        }

        if (!_isStopTimeScale)
        {
            return;
        }

        _isStopTimeScale = false;

        if (_timeScaleController != null)
        {
            _timeScaleController.Play();
        }
    }

    protected virtual void StopTimeScale()
    {
        _slowMoManager.PauseSlowMo();

        if (_isStopTimeScale)
        {
            return;
        }

        _isStopTimeScale = true;

        if (_timeScaleController != null)
        {
            _timeScaleController.Stop();
        }
    }

    #endregion TIMESCALE
}
