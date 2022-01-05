using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathLineController : MonoBehaviour
{
    [Header("Color")]
    [SerializeField] private Color _pointColor = Color.white;

    [Header("Transforms")]
    [SerializeField] private Transform _instanceTransform = null;

    [Header("Settings")]
    [SerializeField] private float _pointDistance = 1.2f;
    [SerializeField] private int _pointCount = 3;

    private Vector3 _rootPosition => _instanceTransform.position;

    public List<Vector3> LinePoints { get; set; }

    public Vector3 GetUniquePoint(int pointId) 
    {
        LinePoints = DefinePositions();

        var a = LinePoints[pointId % _pointCount];
        
        return a; 
    }

    private List<Vector3> DefinePositions()
    {
        List<Vector3> pointPositions = new List<Vector3>();

        if (_instanceTransform != null)
        {
            Vector3 startPoint = _rootPosition + (_pointDistance * -_instanceTransform.right) * (Mathf.RoundToInt(_pointCount / 2));

            pointPositions.Add(startPoint);

            for (int i = 0; i < _pointCount - 1; i++)
            {
                Vector3 pointPosition = startPoint + (_pointDistance * _instanceTransform.right);
                startPoint = pointPosition;

                pointPositions.Add(pointPosition);
            }
        }

        return pointPositions;
    }

#if UNITY_EDITOR
    private void OnDrawGizmos()
    {
        List<Vector3> pointPositions = DefinePositions();
        
        Gizmos.color = _pointColor;

        foreach (Vector3 pointPosition in pointPositions)
        {
            Gizmos.DrawSphere(pointPosition, 0.5f);
        }
    }
#endif
}
