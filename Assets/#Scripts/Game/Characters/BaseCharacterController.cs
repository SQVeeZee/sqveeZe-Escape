using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseCharacterController : MonoBehaviour
{
    public event Action<BaseCharacterController> onCharacterFinishPath = null;
    public event Action<BaseCharacterController> onCharacterDie = null;

    [SerializeField] private ECharacterTeam _characterTeam = ECharacterTeam.NONE;

    [SerializeField] private CharacterMove _characterMove = null;
    [SerializeField] private CharacterAnimatorBase _characterAnimatorBase = null;
    [SerializeField] private BaseCharacterHealth _baseCharacterHealth = null;

    [Header("Transform")]
    [SerializeField] private Transform _characterTransform = null;

    public ECharacterTeam GetCharacterTeam => _characterTeam;
    public BaseCharacterHealth BaseCharacterHealth => _baseCharacterHealth;

    private void OnEnable()
    {
        _characterMove.onCharacterFinish += OnCharacterCompletePath;
        _baseCharacterHealth.onCharacterDie += delegate { onCharacterDie?.Invoke(this); };
    }

    private void OnDisable()
    {
        _characterMove.onCharacterFinish -= OnCharacterCompletePath;
        _baseCharacterHealth.onCharacterDie -= delegate { onCharacterDie?.Invoke(this); };
    }

    private void FixedUpdate()
    {
        _characterMove.TryToMoveCharacter();
    }

    private void Update()
    {
        _characterMove.TryToRotateCharacter();
    }

    public void Initialize(List<Vector3> movePath)
    {
        _characterMove.FillMovePath(movePath);

        _characterMove.RotateImmediately();
    }

    public void OnCharacterCompletePath()
    {
        _characterAnimatorBase.PlayIdle();

        onCharacterFinishPath?.Invoke(this);
    }

    public void StartLastMove(Transform targetPoint, Transform jumpPosition, Action callback)
    {
        _characterTransform.DOMove(targetPoint.position, 2).OnComplete(delegate { DoLastJump(jumpPosition, callback); });
    }

    private void DoLastJump(Transform finalTransform, Action callback)
    {
        _characterMove.JumpOnFinalPlatform(finalTransform, callback);
    }
}
