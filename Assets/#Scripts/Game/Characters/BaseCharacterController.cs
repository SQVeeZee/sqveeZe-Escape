using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseCharacterController : MonoBehaviour
{
    public event Action<BaseCharacterController,int> onReachPoint = null;

    [SerializeField] protected Transform _characterTransform = null;
    [SerializeField] protected Rigidbody _characterRigidbody = null;

    [SerializeField] protected float _moveSpeed = 5f;

    private int _currentGroupId = 0;

    protected Transform _targetTransform = null;
    public Vector3 GetCharacterPosition => _characterTransform.position;
    public Vector3 TargetPosition { get; set; }
    public Vector3 MoveDirection => (TargetPosition - GetCharacterPosition).normalized;

    protected bool _shouldMove = false;

    public void SetTargetTransform(Vector3 targetPosition)
    {
        TargetPosition = targetPosition;
    }

    private void FixedUpdate()
    {
        if (ShouldMoveToTarget())
        {
            Move();
        }
    }

    private void Update()
    {
        if (ShouldMoveToTarget())
        {
            Rotate();
        }
    }

    private void Move()
    {
        Vector3 velocity = MoveDirection;

        velocity.y = 0;

        _characterRigidbody.velocity = velocity * Time.fixedDeltaTime * _moveSpeed;
    }

    private void Rotate()
    {
        Vector3 targetRotation = new Vector3(TargetPosition.x, _characterTransform.position.y, TargetPosition.z);

        _characterTransform.LookAt(targetRotation);
    }

    private bool ShouldMoveToTarget()
    {
        bool shouldMove = true;

        if (Vector3.Distance(GetCharacterPosition, TargetPosition) <= 1.5f)
        {
            shouldMove = false;

            _currentGroupId++;

            onReachPoint?.Invoke(this, _currentGroupId);
        }

        return shouldMove;
    }

    private void OnCompleteMove()
    {

    }

    private void StopMove()
    {
        _characterRigidbody.velocity = Vector3.zero;
    }
}
