using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIGameScreen : BaseScreen
{
    [Header("Screen")]
    [SerializeField] private Image _raycastImage = null;
    [SerializeField] private UIGameClickControls _gameClickControls = null;

    [SerializeField] private Canvas _canvas = null;

    private void Awake()
    {
        _gameClickControls.ScaleFactor = _canvas.scaleFactor;
    }

    public override void Hide(bool force = false, Action callback = null)
    {
        _raycastImage.enabled = false;

        base.Hide(force, callback);
    }
    
    public override void Show(bool force = false, Action callback = null)
    {
        _raycastImage.enabled = true;

        base.Show(force, callback);
    }
}
