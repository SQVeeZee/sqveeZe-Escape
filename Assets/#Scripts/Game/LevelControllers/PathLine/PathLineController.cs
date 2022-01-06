using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathLineController : MonoBehaviour
{
    [Header("Color")]
    [SerializeField] private Color _pointColor = Color.white;

    [Header("Transforms")]
    [SerializeField] private Transform _rootTransform = null;

    //[Header("Settings")]
    //[SerializeField] private float _pointDistance = 1.2f;
    private int _pointCount => _curve.length;

    private Vector3 _rootPosition => _rootTransform.position;

    public List<Vector3> LinePoints { get; set; }

    public Vector3 GetUniquePoint(int pointId) 
    {
        LinePoints = DefinePositions();

        var a = LinePoints[pointId % _pointCount];
        
        return a; 
    }

    [SerializeField] private AnimationCurve _curve;
    [SerializeField] private float _multiplayer = 1;
    private List<Vector3> DefinePositions()
    {
        List<Vector3> pointPositions = new List<Vector3>();
        
        if (_rootTransform != null)
        {
            Vector3 startPoint = _rootPosition;

            for (int i = 0; i < _pointCount; i++)
            {
                Vector3 pointPosition = startPoint + new Vector3(
                    (_curve.keys[i].time) * Mathf.Cos(Mathf.Deg2Rad * -_rootTransform.eulerAngles.y) - (_curve.keys[i].value) * Mathf.Sin(Mathf.Deg2Rad * -_rootTransform.eulerAngles.y)

                    , 0,
                    (_curve.keys[i].time) * Mathf.Sin(Mathf.Deg2Rad * -_rootTransform.eulerAngles.y) + (_curve.keys[i].value) * Mathf.Cos(Mathf.Deg2Rad * -_rootTransform.eulerAngles.y)
                    ) * _multiplayer;

                pointPositions.Add(pointPosition);
            }
        }

        return pointPositions;
    }

    /*private List<Vector3> DefinePositions()
    {
        List<Vector3> pointPositions = new List<Vector3>();
        
        increaser = 0;
        
        if (_instanceTransform != null)
        {
            Vector3 startPoint = _rootPosition + GetPoint();

            pointPositions.Add(startPoint);

            for (int i = 0; i < _pointCount - 1; i++)
            {
                Vector3 pointPosition = startPoint + GetPoint();

                //startPoint = pointPosition;

                pointPositions.Add(pointPosition);

                increaser += increaserPower;
            }
        }

        return pointPositions;
    }
    [SerializeField] private float r = 1f;
    [SerializeField] private float _arcRotation = 1f;

    private float increaser = 0;
    [SerializeField] private float increaserPower = 0.1f;
    private Vector3 GetPoint()
    {
        float angle = _arcRotation + increaser;

        Vector3 pointPosition = new Vector3(r * Mathf.Cos(angle), 0, r * Mathf.Sin(angle));

        return pointPosition;
    }*/

    /*private List<Vector3> DefinePositions()
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
    }*/

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
