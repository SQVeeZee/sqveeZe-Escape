using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelCharactersController : MonoBehaviour
{
    public event Action<ELevelCompleteReason> onLevelCompleted;
    public event Action<BaseCharacterController, Action> onCharacterCompletePath;

    [Header("Transforms")]
    [SerializeField] private Transform _playerRoot = null;
    [SerializeField] private Transform _enemyRoot = null;

    private List<BaseCharacterController> _aliveCharacterControllers = new List<BaseCharacterController>();

    public void FillCharacterInfo(BaseCharacterController characterController)
    {
        SetCharacterRoot(characterController);

        if (characterController.GetCharacterTeam == ECharacterTeam.PLAYER)
        {
            _aliveCharacterControllers.Add(characterController);
        }

        characterController.onCharacterFinishPath += IsCharacterCompletePath;
        characterController.onCharacterDie += OnCharacterDie;
    }

    private void SetCharacterRoot(BaseCharacterController characterController)
    {
        ECharacterTeam characterTeam = characterController.GetCharacterTeam;
        Transform characterRoot = null;

        switch (characterTeam)
        {
            case ECharacterTeam.PLAYER:
                characterRoot = _playerRoot;
                break;
            
            case ECharacterTeam.ENEMY:
                characterRoot = _enemyRoot;
                break;
        }

        characterController.SetCharacterRoot(characterRoot);
    }

    private void IsCharacterCompletePath(BaseCharacterController finishedCharacter)
    {
        onCharacterCompletePath?.Invoke(finishedCharacter, delegate { OnJump(finishedCharacter); });

        void OnJump(BaseCharacterController character)
        {
            _aliveCharacterControllers.Remove(character);

            Destroy(character.gameObject);

            if (_aliveCharacterControllers.Count == 0)
            {
                onLevelCompleted?.Invoke(ELevelCompleteReason.WIN);
            }
        }
    }

    public void OnCharacterDie(BaseCharacterController characterController)
    {
        if (characterController.GetCharacterTeam == ECharacterTeam.PLAYER)
        {
            _aliveCharacterControllers.Remove(characterController);

            if (_aliveCharacterControllers.Count == 0)
            {
                onLevelCompleted?.Invoke(ELevelCompleteReason.LOSE);
            }
        }

        Destroy(characterController.gameObject);
    }
}
