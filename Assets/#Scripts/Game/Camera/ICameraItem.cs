using System;
using System.Collections.Generic;
using UnityEngine;

public interface ICameraItem
{
    Camera Camera { get; }
    ECameraType CameraType { get; }

    void ChangePosition(Transform targetTransform, Action callback = null);
    void MoveToPoint(Vector3 position, Vector3 rotation, Action callback = null);
    void MoveOnPath(List<Transform> points);
}
