using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class GunController : MonoBehaviour
{
    public event Action onHit;

    private Camera _gameCamera = null;

    private Ray ray = default;
    private float nextFire = 0f;

    private bool _isDragging;

    [Inject] protected readonly IGunConfigs _gunConfigs = null;
    [Inject] protected readonly UIGameClickControls _clickControls = null;

    [Inject]
    protected virtual void Initialize(UIGameClickControls clickControls)
    {
        clickControls.onPointerDrag += OnClick;
        clickControls.onPointerDown += OnPointerDown;
        clickControls.onPointerUp += OnPointerUp;
    }

    private void Start()
    {
        _gameCamera = CameraManager.Instance.GetCameraItem(ECameraType.GAME).Camera;
    }

    protected virtual void OnClick(Vector2 viewportPosition)
    {
        var clickRay = _gameCamera.ScreenPointToRay(viewportPosition);
        ray = clickRay;
    }

    private void OnPointerDown(Vector2 viewportPosition)
    {
        _isDragging = true;

        OnClick(viewportPosition);
    }

    private void OnPointerUp(Vector2 viewportPosition)
    {
        _isDragging = false;

        OnClick(viewportPosition);
    }


    private void Update()
    {
        if (_isDragging)
        {
            Fire(ray);
        }
    }

    private void Fire(Ray ray)
    {
        if (Time.time >= nextFire)
        {
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, 1000))
            {
                BaseCharacterController characterController = hit.transform.GetComponent<BaseCharacterController>();
                ExplosionBarrel _explosionBarrel = hit.transform.GetComponent<ExplosionBarrel>();
                WoodenPlane _woodenPlane = hit.transform.GetComponent<WoodenPlane>();

                if (characterController != null && characterController.GetCharacterTeam == ECharacterTeam.ENEMY)
                {
                    characterController.BaseCharacterHealth.KillCharacter();

                    OnHit();
                }
                else if (_explosionBarrel != null)
                {
                    _explosionBarrel.BlowUpBarrel();

                    OnHit();
                }
                else if (_woodenPlane != null)
                {
                    _woodenPlane.BreakPlanes();
                    OnHit();
                }
            }
        }

        void OnHit()
        {
            nextFire = Time.time + _gunConfigs.FireRate;
            onHit?.Invoke();
        }
    }
}
