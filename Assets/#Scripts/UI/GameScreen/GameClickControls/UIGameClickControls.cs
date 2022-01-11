using System;
using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;
using Zenject;

public class UIGameClickControls : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IDragHandler
{
    public event Action<Vector2> onPointerDown = null;
    public event Action<Vector2> onPointerDrag = null;
    public event Action<Vector2> onPointerUp = null;

    [Header("RectTransform")]
    [SerializeField] private RectTransform _aimTransform = null;

    [Space]
    [SerializeField] private GameObject _hitMark = null;

    public bool IsActive { get; protected set; }
    public bool IsDragging { get; private set; } = false;

    public float ScaleFactor { get; set; } = default;

    private Camera _uiCamera = null;
    private Coroutine _hitMarkCoroutine = null;

    private Vector2 _screenSize;
    private Vector2 _targetResolution;

    private Vector2 _resolutionDifference => (_targetResolution / _screenSize);

    [Inject]
    protected virtual void Initialize(GunController gunController)
    {
        gunController.onHit += AnimateHitMarkOnHit;
    }


    private void Start()
    {
        DefineScreenValues();

        SetHitMarkState(false);

        _uiCamera = CameraManager.Instance.GetCameraItem(ECameraType.UI).Camera;
    }

    private Vector2 ConvertPositionScreenToViewport(Vector2 value)
    {
        Vector2 result = _uiCamera.ScreenToViewportPoint(value);
        return result;
    }

    private void OnDisable()
    {
        IsDragging = false;
        SetHitMarkState(false);
    }

    private void AnimateHitMarkOnHit()
    {
        if (!IsDragging) return;

        _hitMarkCoroutine = StartCoroutine(DoHitMarkAnimation());
    }

    private void StopCoroutine()
    {
        if (_hitMarkCoroutine != null)
        {
            StopCoroutine(_hitMarkCoroutine);
        }
    }

    private void SetHitMarkState(bool state)
    {
        _hitMark.SetActive(state);
    }
    private IEnumerator DoHitMarkAnimation()
    {
        SetHitMarkState(true);

        yield return new WaitForSeconds(0.1f);

        SetHitMarkState(false);
    }

    
    private void DefineScreenValues()
    {
        _screenSize = new Vector2(Screen.width, Screen.height);
        _targetResolution = new Vector2(1440, 2560);
    }

    public void OnDrag(PointerEventData eventData)
    {
        Vector2 finalPosition = _aimTransform.anchoredPosition += eventData.delta * _resolutionDifference;

        onPointerDrag?.Invoke(_aimTransform.anchoredPosition * ScaleFactor);
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        OnPointerDown(_aimTransform.anchoredPosition * ScaleFactor);
    }

    private void OnPointerDown(Vector2 eventData)
    {
        IsDragging = true;

        onPointerDown?.Invoke(eventData);
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        OnPointerUp(_aimTransform.anchoredPosition * ScaleFactor);
    }

    private void OnPointerUp(Vector2 eventData)
    {
        IsDragging = false;

        onPointerUp?.Invoke(eventData);
    }
}
