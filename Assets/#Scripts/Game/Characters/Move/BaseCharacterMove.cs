using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseCharacterMove : MonoBehaviour
{
    public event Action onCharacterFinish = null;

    [Header("Ground check")]
    [SerializeField] protected CharacterGroundCheck _characterGroundCheck = null;

    [Space]
    [SerializeField] protected Transform _characterTransform = null;
    [SerializeField] protected Transform _rotationTransform = null;
    [SerializeField] protected Rigidbody _characterRigidbody = null;

    [Header("Settings")]
    [SerializeField] protected float _moveSpeed = 5f;
    [SerializeField] protected float _rotationSpeed = 5f;

    private List<Vector3> _movePoints = new List<Vector3>();
    private int _currentPointId = 0;
    private bool _shouldMove = true;

    public Vector3 MoveDirection => ((_rotationTransform.forward + _rotationTransform.position) - _rotationTransform.position).normalized;
    public Vector3 GetCharacterPosition => _characterTransform.position;
    public Vector3 TargetPosition => _movePoints[_currentPointId];

    public bool ShouldMove 
    { 
        get
        {
            return _shouldMove;
        }
        set
        {
            _shouldMove = value;

            if (!_shouldMove)
            {
                StopMove();
            }
        }
    }
    public virtual void MoveAfterPathCompleted(Transform finalTransform, Action callback)
    {
        ShouldMove = false;

    }

    public void FillMovePath(List<Vector3> movePath) => _movePoints = movePath;

    public void AddPointToPath(Vector3 point)
    {
        _movePoints.Add(point);
    }

    public void TryToMoveCharacter()
    {
        if (!ShouldMove) return;

        if (ShouldMoveToTarget() && _characterGroundCheck.IsGrounded())
        {
            Rotate();
            Move();
        }
    }

    public void RotateImmediately()
    {
        _rotationTransform.LookAt(TargetPosition);
    }

    private void Move()
    {
        Vector3 velocity = MoveDirection;

        velocity.y = 0;

        _characterRigidbody.velocity = velocity * _moveSpeed * Time.fixedDeltaTime;

        if (IsCloseToTargetPoint())
        {
            _currentPointId++;
        }
    }

    private void Rotate()
    {
        Vector3 direction = TargetPosition - _rotationTransform.position;
        direction.y = 0;

        Vector3 newDir = Vector3.RotateTowards(_rotationTransform.forward, direction, Time.deltaTime, 0.0F);

        _rotationTransform.rotation = Quaternion.Lerp(_rotationTransform.rotation, Quaternion.LookRotation(newDir),
            _rotationSpeed * Time.deltaTime);
    }

    private bool ShouldMoveToTarget()
    {
        ShouldMove = _currentPointId < _movePoints.Count;

        if (!ShouldMove)
        {
            onCharacterFinish?.Invoke();
        }

        return ShouldMove;
    }

    private bool IsCloseToTargetPoint()
    {
        bool isCloseToPoint = false;

        if (Vector3.Distance(GetCharacterPosition, TargetPosition) <= 1.5f)
        {
            isCloseToPoint = true;
        }

        return isCloseToPoint;
    }

    protected void StopMove() => _characterRigidbody.velocity = Vector3.zero;
}
