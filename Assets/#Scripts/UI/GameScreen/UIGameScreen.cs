using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIGameScreen : BaseScreen
{
    [SerializeField] private Image _raycastImage = null;

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
