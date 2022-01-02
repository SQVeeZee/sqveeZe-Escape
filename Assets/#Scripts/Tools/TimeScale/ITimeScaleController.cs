using UnityEngine;

public interface ITimeScaleController
{
    float GetDefaultTimeScale { get; }
    float GetDefaultFixedDeltaTime { get; }

    float GetTimeScale { get; }
    float GetFixedDeltaTime { get; }


    void SetTimeScale(float value);

    void SetFixedDeltaTime(float value);

    void Stop();

    void Play(bool force = false);
}
