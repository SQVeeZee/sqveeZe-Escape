using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraItem : MonoBehaviour, ICameraItem
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

        DoMove(points[pointId].position, points[pointId].eulerAngles, OnReachPoint);

        void OnReachPoint()
        {
            pointId++;

            if (pointId >= points.Count) return;

            DoMove(points[pointId].position, points[pointId].eulerAngles, OnReachPoint);
        }
    }

    public void MoveToPoint(Vector3 position, Vector3 rotation, Action callback = null)
    {
        ResetTween();

        DoMove(position, rotation, callback);
    }

    protected void DoMove(Vector3 position, Vector3 rotation, Action callback = null)
    {
        ResetTween();

        float duration = Mathf.Abs((position - CameraPosition).magnitude) / _speed;

        _moveTween = _cameraTransform.DOMove(position, duration).SetEase(Ease.Linear);
        _cameraTransform.DORotate(rotation, duration).SetEase(Ease.Linear).OnComplete(delegate { callback?.Invoke(); });
    }

    private void ResetTween()
    {
        _moveTween?.Kill();
        _moveTween = null;
        
        _rotateTween?.Kill();
        _rotateTween = null;
    }
}
