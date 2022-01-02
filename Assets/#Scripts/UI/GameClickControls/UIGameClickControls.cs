using System;
using SkyRocket.Project.Game;
using UnityEngine;
using UnityEngine.EventSystems;

namespace SkyRocket.Project.Game.UI
{
    public class UIGameClickControls : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
    {
        public event Action<Vector2> onPointerDown = null;
        public event Action<Vector2> onPointerUp = null;
        
        
        [SerializeField] private GameObject _gameObject = null;


        public bool IsActive { get; protected set; }
        
        
        private Camera _uiCamera = null;
        
        private bool _isDown = false;
        

        /*private void Start()
        {
            _uiCamera = CameraManager.Instance.GetCamera(ECameraType.UI).Camera;
        }*/

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

#region OnPointerDown

        public void OnPointerDown(PointerEventData eventData)
        {
            var screenPosition = eventData.position;
            var position = ConvertPositionScreenToViewport(screenPosition);
            OnPointerDown(position);
        }

        private void OnPointerDown(Vector2 eventData)
        {
            _isDown = true;
            
            onPointerDown?.Invoke(eventData);
        }
        
#endregion OnPointerDown

#region OnPointerDown

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
        
#endregion OnPointerDown
    }
}
