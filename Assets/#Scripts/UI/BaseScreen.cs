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

    [SerializeField] protected GameObject _canvasObject = null;
    [SerializeField] protected CanvasGroup _canvasGroup = null;

    [Header("Settings")]
    [SerializeField] private float _hideDuration = 0.5f;
    [SerializeField] private float _showDuration = 0.5f;

    public virtual void Hide(bool force = false, Action callback = null)
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
            _canvasObject.SetActive(false);

            onAfterHide?.Invoke();

            callback?.Invoke();
        }
    }

    public virtual void Show(bool force = false, Action callback = null)
    {
        onBeforeShow?.Invoke();

        _canvasObject.SetActive(true);

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
            onAfterShow?.Invoke();

            callback?.Invoke();
        }
    }
}
