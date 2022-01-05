using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraItem : MonoBehaviour,ICameraItem
{
    [SerializeField] private ECameraType _cameraType = ECameraType.NONE;
    [SerializeField] private Camera _camera = null;
    [SerializeField] private Transform _cameraTransform = null;
    [SerializeField] private float _speed = 10;
    public ECameraType CameraType { get => _cameraType; }
    public Camera Camera => _camera;
    public Vector3 CameraPosition => _cameraTransform.position;

    private Tweener _moveTween = null;
    private Tweener _rotateTween = null;
    /*public void MoveToTargetPoint(Vector3 targetPoint,Vector3 targetRotation)
    {
        float duration = Mathf.Abs((targetPoint - CameraPosition).magnitude) / _speed;

        _cameraTransform.DOMove(targetPoint, duration);
        _cameraTransform.DORotate(targetRotation, duration);
    }*/

    public void ChangePosition(Transform targetTransform, Action callback = null)
    {
        ResetTween();

        _cameraTransform.position = targetTransform.position;
        _cameraTransform.eulerAngles = targetTransform.eulerAngles;

        callback?.Invoke();
    }

    public void MoveOnPath(List<Transform> points)
    {
        ResetTween();

        int pointId = 0;

        Move(points[pointId].position, points[pointId].eulerAngles, OnReachPoint);

        void OnReachPoint()
        {
            pointId++;

            if (pointId >= points.Count) return;

            Move(points[pointId].position, points[pointId].eulerAngles, OnReachPoint);
        }
    }

    private void Move(Vector3 position, Vector3 rotation, Action callback = null)
    {
        ResetTween();

        float duration = Mathf.Abs((position - CameraPosition).magnitude) / _speed;

        _moveTween = _cameraTransform.DOMove(position, duration);
        _cameraTransform.DORotate(rotation, duration).OnComplete(delegate { callback?.Invoke(); });
    }

    private void ResetTween()
    {
        _moveTween?.Kill();
        _moveTween = null;
        
        _rotateTween?.Kill();
        _rotateTween = null;
    }
}
