using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class SlowMoController : MonoBehaviour
{
    [Inject]
    protected virtual void Initialize(LevelsController levelsController)
    {
        levelsController.onSlowMoActivated += StartSlowMo;
        levelsController.onSlowMoDisabled += ResetSlowMo;
    }

    private void StartSlowMo()
    {
        Time.timeScale = 0.3f;
    }

    private void ResetSlowMo()
    {
        Time.timeScale = 1f;
    }
}
