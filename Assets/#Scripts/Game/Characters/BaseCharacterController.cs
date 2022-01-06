using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseCharacterController : MonoBehaviour
{
    public event Action<BaseCharacterController> onCharacterFinishPath = null;
    public event Action<BaseCharacterController> onCharacterDie = null;

    [Header("Team")]
    [SerializeField] protected ECharacterTeam _characterTeam = ECharacterTeam.NONE;

    [Space]
    [SerializeField] protected BaseCharacterMove _characterMove = null;
    [SerializeField] protected CharacterAnimatorBase _characterAnimatorBase = null;
    [SerializeField] protected BaseCharacterHealth _baseCharacterHealth = null;

    [Space]
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

    private void Update()
    {
        _characterMove.TryToMoveCharacter();
    }

    public void Initialize(List<Vector3> movePath)
    {
        _characterMove.FillMovePath(movePath);

        _characterMove.RotateImmediately();
    }

    public virtual void OnCharacterCompletePath()
    {
        onCharacterFinishPath?.Invoke(this);
    }

    public void DoMoveAfterPathCompleted(Transform finalTransform, Action callback)
    {
        _characterMove.MoveAfterPathCompleted(finalTransform, callback);
    }

    public void SetCharacterRoot(Transform characterRoot)
    {
        _characterTransform.SetParent(characterRoot);
    }

    public void SetLastMovePathPoint(Transform lastMovePathPoint)
    {
        _characterMove.AddPointToPath(lastMovePathPoint.position);
    }
}
