using System;
using UnityEngine;
using Zenject;

public class SlowMoManager : MonoSingleton<SlowMoManager>
{
    public event Action<bool> onSlowMoActive = null;

    [Inject] protected readonly TimeScaleController _timeScaleController = null;

    public bool IsActive { get; protected set; }

    protected float _defaultTimeScale = 1f;
    protected float _defaultFixedDeltaTime = 0.02f;

    protected float _currenTimeScale = 0f;
    protected float _currenFixedDeltaTime = 0f;
    protected float _currentFactorSlowMo = 0f;
    protected float _currentLengthSlowMo = 0f;

    protected override void Awake()
    {
        base.Awake();

        enabled = false;
    }

    protected override void OnDestroy()
    {
        base.OnDestroy();

        UnpauseSlowMo();
    }

    protected virtual void Update()
    {
        float delta = (_defaultTimeScale / _currentLengthSlowMo) * Time.unscaledDeltaTime;

        float timeScale = _timeScaleController.GetTimeScale;
        timeScale += delta;
        timeScale = Mathf.Clamp(timeScale, 0f, _defaultTimeScale);

        _timeScaleController.SetTimeScale(timeScale);
        _currenTimeScale = timeScale;

        float fixedDeltaTime = timeScale * (_defaultFixedDeltaTime / _defaultTimeScale);
        _timeScaleController.SetFixedDeltaTime(fixedDeltaTime);
        _currenFixedDeltaTime = fixedDeltaTime;

        if (timeScale >= _defaultTimeScale)
        {
            enabled = false;
            IsActive = false;
        }
    }

    public virtual void DoSlowMo(float factor, float length)
    {
        _defaultTimeScale = _timeScaleController.GetDefaultTimeScale;
        _defaultFixedDeltaTime = _timeScaleController.GetDefaultFixedDeltaTime;

        _currentFactorSlowMo = factor;
        _currentLengthSlowMo = length;

        _currenTimeScale = factor;
        _timeScaleController.SetTimeScale(factor);

        float defaultFixedDeltaTime = _defaultFixedDeltaTime;
        float fixedDeltaTime = factor * (defaultFixedDeltaTime / _defaultTimeScale);
        _timeScaleController.SetFixedDeltaTime(fixedDeltaTime);
        _currenFixedDeltaTime = fixedDeltaTime;

        enabled = true;
        IsActive = true;

        onSlowMoActive?.Invoke(true);
    }

    public virtual void ResetSlowMo()
    {
        if (!IsActive)
        {
            return;
        }

        float defaultTimeScale = _timeScaleController.GetDefaultTimeScale;
        _timeScaleController.SetTimeScale(defaultTimeScale);

        float defaultFixedDeltaTime = _timeScaleController.GetDefaultFixedDeltaTime;
        _timeScaleController.SetFixedDeltaTime(defaultFixedDeltaTime);

        enabled = false;
        IsActive = false;

        onSlowMoActive?.Invoke(false);
    }

    public virtual void PauseSlowMo()
    {
        enabled = false;
    }

    public virtual void UnpauseSlowMo()
    {
        if (IsActive)
        {
            if (_timeScaleController != null)
            {
                _timeScaleController.SetTimeScale(_currenTimeScale);
                _timeScaleController.SetFixedDeltaTime(_currenFixedDeltaTime);
            }

            if (this != null)
            {
                enabled = true;
            }
        }
    }
}
