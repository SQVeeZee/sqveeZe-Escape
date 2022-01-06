using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class SlowMoController : MonoBehaviour
{
    [Inject] UITransitionScreen _transitionScreen = null;

    public void StartSlowMo()
    {
        Time.timeScale = 0.5f;
    }

    public void ResetSlowMo(Action callback = null)
    {
        _transitionScreen.Show(false, OnCompleteTransition);

        void OnCompleteTransition()
        {
            callback?.Invoke();
            Time.timeScale = 1f;
        }
    }
}
