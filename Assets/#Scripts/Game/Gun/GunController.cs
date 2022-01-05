using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class GunController : MonoBehaviour
{
    Camera _gameCamera = null;

    [Inject]
    protected virtual void Initialize(UIGameClickControls clickControls)
    {
        clickControls.onPointerDrag += OnClick;
    }

    private void Start()
    {
        _gameCamera = CameraManager.Instance.GetCameraItem(ECameraType.GAME).Camera;
    }

    protected virtual void OnClick(Vector2 viewportPosition)
    {
        var clickRay = _gameCamera.ScreenPointToRay(viewportPosition);

        Fire(clickRay);
    }

    private void Fire(Ray ray)
    {
        Debug.DrawRay(ray.origin, ray.direction * 10, Color.blue);

        RaycastHit hit;

        if (Physics.Raycast(ray,out hit, 1000))
        {
            BaseCharacterController characterController = hit.transform.GetComponent<BaseCharacterController>();

            if (characterController != null && characterController.GetCharacterTeam == ECharacterTeam.ENEMY)
            {
                characterController.BaseCharacterHealth.KillCharacter();
            }
            else
            {
                ExplosionBarrel _explosionBarrel = hit.transform.GetComponent<ExplosionBarrel>();
                
                if (_explosionBarrel != null)
                {
                    _explosionBarrel.BlowUpBarrel();
                }
            }
        }
    }
}
