using UnityEngine;

public class TimeScaleController : ZenjectSingleton<TimeScaleController>, ITimeScaleController
{
    protected readonly float _defaultTimeScale = Time.timeScale;
    protected readonly float _defaultFixedDeltaTime = Time.fixedDeltaTime;


    protected int _stopCount = 0;
    protected float _stopTimeScale = 1f;


    public virtual float GetDefaultTimeScale => _defaultTimeScale;
    public virtual float GetDefaultFixedDeltaTime => _defaultFixedDeltaTime;

    public virtual float GetTimeScale => Time.timeScale;
    public virtual float GetFixedDeltaTime => Time.fixedDeltaTime;


    public virtual void SetTimeScale(float value)
    {
        Time.timeScale = value;

        // Debug.LogFormat("TimeScaleController -> SetTimeScale -> value = {0}", value);
    }

    public virtual void SetFixedDeltaTime(float value)
    {
        Time.fixedDeltaTime = value;

        // Debug.LogFormat("TimeScaleController -> SeFixedDeltaTime -> value = {0}", value);
    }

    public virtual void Stop()
    {
        Debug.LogFormat("TimeScaleController -> Stop -> _stopCount = {0}", _stopCount);

        if (_stopCount == 0)
        {
            _stopTimeScale = GetTimeScale;
            SetTimeScale(0f);
        }

        _stopCount++;
    }

    public virtual void Play(bool force = false)
    {
        Debug.LogFormat("TimeScaleController -> Play -> _stopCount = {0}", _stopCount);

        _stopCount--;

        if (_stopCount < 0)
        {
            _stopCount = 0;
        }

        if (force)
        {
            _stopCount = 0;
        }

        if (_stopCount == 0)
        {
            SetTimeScale(_stopTimeScale);
        }
    }
}
