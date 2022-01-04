using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IScreen
{
    public event Action onBeforeShow;
    public event Action onAfterShow;
    public event Action onBeforeHide;
    public event Action onAfterHide;

    void Show(bool force = false, Action callback = null);
    void Hide(bool force = false, Action callback = null);
}
