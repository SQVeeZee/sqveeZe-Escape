using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseScreen : MonoBehaviour, IScreen
{
    public event Action onBeforeShow;
    public event Action onAfterShow;
    public event Action onBeforeHide;
    public event Action onAfterHide;

    [Header("Screen")]
    [SerializeField] protected GameObject _canvas = null;
    [SerializeField] protected CanvasGroup _canvasGroup = null;

    [Header("Settings")]
    [SerializeField] private float _hideDuration = 0.5f;
    [SerializeField] private float _showDuration = 0.5f;

    public void Hide(bool force = false, Action callback = null)
    {
        onBeforeHide?.Invoke();

        if (force)
        {
            _canvasGroup.alpha = 0;

            OnHide();
        }
        else
        {
            _canvasGroup.DOFade(0,_hideDuration).OnComplete(OnHide);
        }

        void OnHide()
        {
            _canvas.SetActive(false);

            onAfterHide?.Invoke();
        }
    }

    public void Show(bool force = false, Action callback = null)
    {
        onBeforeShow?.Invoke();

        if (force)
        {
            _canvasGroup.alpha = 1;

            OnShow();
        }
        else
        {
            _canvasGroup.DOFade(1, _showDuration).OnComplete(OnShow);
        }

        void OnShow()
        {
            _canvas.SetActive(true);

            onAfterShow?.Invoke();
        }
    }
}
