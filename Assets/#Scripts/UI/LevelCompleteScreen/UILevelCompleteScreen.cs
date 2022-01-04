using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class UILevelCompleteScreen : BaseScreen
{
    public event Action<ELevelCompleteReason> onCompleteLevel = null;

    [Header("Buttons")]
    [SerializeField] private Button _nextButton = null;

    [Inject] LevelsController _levelsController = null;

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
        _nextButton.onClick.AddListener(StartNextLevel);
    }
    private void RemoveListeners()
    {
        _nextButton?.onClick.RemoveListener(StartNextLevel);
    }

    private void StartNextLevel()
    {
        Hide();

        onCompleteLevel?.Invoke(ELevelCompleteReason.WIN);
    }
}
