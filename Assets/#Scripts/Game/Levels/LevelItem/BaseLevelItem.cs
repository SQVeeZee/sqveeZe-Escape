using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public abstract class BaseLevelItem : MonoBehaviour, ILevelItem
{
    public event Action onPathCompleted;
    public event Action<ELevelCompleteReason> onLevelCompleted;

    [Header("Rooms")]
    [SerializeField] private List<CharactersRoomOut> _ñharactersRoomOut = new List<CharactersRoomOut>();

    [Header("Final Platform")]
    [SerializeField] private FinalPlatform _finalPlatform = null;

    private List<BaseCharacterController> _aliveCharacterControllers = new List<BaseCharacterController>();
    private List<BaseCharacterController> _charactersFinished = new List<BaseCharacterController>();

    private void OnEnable()
    {
        foreach(CharactersRoomOut roomOut in _ñharactersRoomOut)
        {
            roomOut.onCreateCharacter += SetupCharacter;
        }
    }
    private void OnDisable()
    {
        foreach (CharactersRoomOut roomOut in _ñharactersRoomOut)
        {
            roomOut.onCreateCharacter -= SetupCharacter;
        }
    }

    private void Awake()
    {
        _ñharactersRoomOut[0].ActivateRoom();
    }

    private void SetupCharacter(BaseCharacterController characterController)
    {
        if (characterController.GetCharacterTeam == ECharacterTeam.PLAYER)
        {
            _aliveCharacterControllers.Add(characterController);
            characterController.onCharacterFinishPath += IsAllCharactersCompletePath;
            characterController.onCharacterDie += OnCharacterDie;
        }
    }

    private void IsAllCharactersCompletePath(BaseCharacterController finishedCharacter)
    {
        _charactersFinished.Add(finishedCharacter);

        if (IsAllCharactersFinished())
        {
            Do();

            onPathCompleted?.Invoke();
        }
    }

    private void OnCharacterDie(BaseCharacterController characterController)
    {
        _aliveCharacterControllers.Remove(characterController);

        characterController.gameObject.SetActive(false);

        if (_aliveCharacterControllers.Count == 0)
        {
            onLevelCompleted?.Invoke(ELevelCompleteReason.LOSE);
        }
    }

    private void Do()
    {
        foreach (BaseCharacterController characterController in _aliveCharacterControllers.ToList())
        {
            characterController.StartLastMove(_finalPlatform.MovePlaceTransform, _finalPlatform.JumpPlaceTransform, 
                delegate { OnJump(characterController); });
        }

        void OnJump(BaseCharacterController character)
        {
            _aliveCharacterControllers.Remove(character);

            if (_aliveCharacterControllers.Count == 0)
            {
                onLevelCompleted?.Invoke(ELevelCompleteReason.WIN);
            }
        }
    }

    private bool IsAllCharactersFinished()
    {
        return _charactersFinished.Count == _aliveCharacterControllers.Count;
    }
}
