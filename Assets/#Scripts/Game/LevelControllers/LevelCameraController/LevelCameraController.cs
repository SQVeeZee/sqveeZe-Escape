using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelCameraController : MonoBehaviour
{
    [Header("Camera")]
    [SerializeField] private List<Transform> _cameraPoints = null;
    [SerializeField] private FinalPlatform _finalPlatform = null;

    [Header("Settings")] private float _startDelay = 3f;

    private CameraItem _gameCameraItem = null;

    public void Initialize()
    {
        _gameCameraItem = (CameraItem)CameraManager.Instance.GetCameraItem(ECameraType.GAME);

        _gameCameraItem.ChangePosition(_cameraPoints[0]);

        StartCoroutine(StartMoveWithDelay());
    }

    public void SetTransformOnSlowMo(Action callback = null)
    {
        Transform slowMoCameraTransform = _finalPlatform.GetSlowMoCameraTransform;

        _gameCameraItem.ChangePosition(slowMoCameraTransform, callback);
    }
    public void SetTransformOnFinal()
    {
        _gameCameraItem.ChangePosition(_finalPlatform.GetFinalCameraTransform);
    }

    private IEnumerator StartMoveWithDelay()
    {
        WaitForSeconds timer = new WaitForSeconds(_startDelay);
        
        yield return timer;

        StartMove();
    }

    private void StartMove()
    {
        _gameCameraItem.MoveOnPath(_cameraPoints);
    }
}
