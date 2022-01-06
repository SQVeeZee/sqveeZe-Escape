using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class UITransitionScreen : BaseScreen
{
    private void Awake()
    {
        Hide(true);
    }

    public override void Show(bool force = false, Action callback = null)
    {
        base.Show(force, A);

        void A()
        {
            callback?.Invoke();

            Hide();
        }
    }
}
