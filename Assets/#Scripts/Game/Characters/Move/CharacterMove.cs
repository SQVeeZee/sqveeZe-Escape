using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMove : MonoBehaviour
{
    public event Action onCharacterFinish = null;

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

    public void FillMovePath(List<Vector3> movePath) => _movePoints = movePath;

    public void TryToMoveCharacter()
    {
        if (!ShouldMove) return;

        if (ShouldMoveToTarget())
        {
            Move();
        }
    }

    public void TryToRotateCharacter()
    {
        if (!ShouldMove) return;

        if (ShouldMoveToTarget())
        {
            Rotate();
        }
    }

    public void RotateImmediately()
    {
        _rotationTransform.LookAt(TargetPosition);
    }

    public void JumpOnFinalPlatform(Transform finalTransform, Action callback)
    {
        _characterTransform.DOJump(finalTransform.position, 1, 1, 1).OnComplete(delegate { callback?.Invoke(); });
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

    private void StopMove() => _characterRigidbody.velocity = Vector3.zero;
}
