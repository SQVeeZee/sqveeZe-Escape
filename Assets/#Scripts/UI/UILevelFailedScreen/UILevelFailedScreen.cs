using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class UILevelFailedScreen : BaseScreen
{
    public event Action<ELevelCompleteReason> onRestart = null;

    [Header("Buttons")]
    [SerializeField] private Button _restartButton = null;

    private void OnEnable()
    {
        AddListeners();
    }

    private void OnDisable()
    {
        RemoveListeners();
    }


    private void Awake()
    {
        Hide(true);
    }

    private void AddListeners()
    {
        _restartButton.onClick.AddListener(RestartLevel);
    }
    private void RemoveListeners()
    {
        _restartButton?.onClick.RemoveListener(RestartLevel);
    }

    private void RestartLevel()
    {
        Hide();

        onRestart?.Invoke(ELevelCompleteReason.LOSE);
    }
}
