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
    public float ScaleFactor = default;

    private Camera _uiCamera = null;
    private Coroutine _hitMarkCoroutine = null;

    [Inject]
    protected virtual void Initialize(GunController gunController)
    {
        gunController.onHit += AnimateHitMarkOnHit;
    }


    private void Start()
    {
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

    public void OnDrag(PointerEventData eventData)
    {
        _aimTransform.anchoredPosition += eventData.delta;

        Vector2 finalPosition = _aimTransform.anchoredPosition * ScaleFactor;

        onPointerDrag?.Invoke(finalPosition);
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        var screenPosition = eventData.position;

        OnPointerDown(screenPosition);
    }

    private void OnPointerDown(Vector2 eventData)
    {
        IsDragging = true;

        onPointerDown?.Invoke(eventData);
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        var screenPosition = eventData.position;
        
        var position = ConvertPositionScreenToViewport(screenPosition);
        
        OnPointerUp(position);
    }

    private void OnPointerUp(Vector2 eventData)
    {
        IsDragging = false;

        onPointerUp?.Invoke(eventData);
    }
}
