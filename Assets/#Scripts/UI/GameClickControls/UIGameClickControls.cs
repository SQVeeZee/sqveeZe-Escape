using System;
using UnityEngine;
using UnityEngine.EventSystems;

public class UIGameClickControls : BaseScreen, IPointerDownHandler, IPointerUpHandler, IDragHandler
{
    public event Action<Vector2> onPointerDown = null;
    public event Action<Vector2> onPointerDrag = null;
    public event Action<Vector2> onPointerUp = null;

    [SerializeField] private RectTransform _imageTransform = null;
    [SerializeField] private RectTransform _aimTransform = null;

    public bool IsActive { get; protected set; }


    private Camera _uiCamera = null;

    private bool _isDown = false;

    private void Awake()
    {
        Show(true);
    }

    private void Start()
    {
        _uiCamera = CameraManager.Instance.GetCameraItem(ECameraType.UI).Camera;
    }

    public void Initialize()
    {
        Restart(false);
    }

    public void Restart(bool full)
    {
        SetActive(false);
    }

    public void SetActive(bool value, bool force = false)
    {
        IsActive = value;

        enabled = value;

        if (_isDown)
        {
            var screenPosition = Input.mousePosition;
            var position = ConvertPositionScreenToViewport(screenPosition);
            OnPointerUp(position);
        }
    }

    private Vector2 ConvertPositionScreenToViewport(Vector2 value)
    {
        Vector2 result = _uiCamera.ScreenToViewportPoint(value);
        return result;
    }

    Vector2 clickedPos = default;

    public void OnDrag(PointerEventData eventData)
    {
        _imageTransform.anchoredPosition += clickedPos + eventData.delta;

        //Debug.Log(_imageTransform.anchoredPosition);

        onPointerDrag?.Invoke(_imageTransform.anchoredPosition);
    }

    #region OnPointerDown

    public void OnPointerDown(PointerEventData eventData)
    {
        var screenPosition = eventData.position;

        var position = ConvertPositionScreenToViewport(screenPosition);

        clickedPos = position;

        OnPointerDown(position);
    }

    private void OnPointerDown(Vector2 eventData)
    {
        _isDown = true;

        onPointerDown?.Invoke(eventData);
    }

    #endregion OnPointerDown

    #region OnPointerUp

    public void OnPointerUp(PointerEventData eventData)
    {
        var screenPosition = eventData.position;
        var position = ConvertPositionScreenToViewport(screenPosition);
        OnPointerUp(position);
    }

    private void OnPointerUp(Vector2 eventData)
    {
        _isDown = false;

        onPointerUp?.Invoke(eventData);
    }

    #endregion OnPointerUp
}
