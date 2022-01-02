using System;
using System.Collections;
using UnityEngine;

public class MonoBehaviourController : MonoCreateSingleton<MonoBehaviourController>
{
    public event Action<bool> onApplicationFocus = null;
    public event Action<bool> onApplicationPause = null;

    public event Action onAwake = null;
    public event Action onStart = null;
    public event Action onLateStart = null;

    public event Action<float> onUpdate = null;
    public event Action<float> onFixedUpdate = null;
    public event Action<float> onLateUpdate = null;

    public event Action onApplicationQuit = null;


    protected override void Awake()
    {
        base.Awake();

        if (IsInstance)
        {
            // SetAsDontDestroy();

            onAwake?.Invoke();
        }
    }

    protected virtual IEnumerator Start()
    {
        onStart?.Invoke();

        yield return null;

        onLateStart?.Invoke();
    }

    private void OnApplicationFocus(bool hasFocus)
    {
        onApplicationFocus?.Invoke(hasFocus);
    }

    private void OnApplicationPause(bool pauseStatus)
    {
        onApplicationPause?.Invoke(pauseStatus);
    }

    private void OnApplicationQuit()
    {
        onApplicationQuit?.Invoke();
    }

    #region UPDATE

    private void Update()
    {
        if (onUpdate == null)
        {
            return;
        }

        float deltaTime = Time.deltaTime;

        onUpdate.Invoke(deltaTime);
    }

    private void FixedUpdate()
    {
        if (onFixedUpdate == null)
        {
            return;
        }

        float deltaTime = Time.fixedDeltaTime;

        onFixedUpdate.Invoke(deltaTime);
    }

    private void LateUpdate()
    {
        if (onLateUpdate == null)
        {
            return;
        }

        float deltaTime = Time.deltaTime;

        onLateUpdate.Invoke(deltaTime);
    }

    #endregion UPDATE
}
