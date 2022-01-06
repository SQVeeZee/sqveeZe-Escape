using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Zenject;

public abstract class BaseLevelItem : MonoBehaviour, ILevelItem
{
    public event Action<Action> onPathCompleted;
    public event Action onSlowMoActive;
    public event Action<ELevelCompleteReason> onLevelCompleted;

    [SerializeField] private LevelCharactersController _levelCharactersController = null;
    [SerializeField] private FinalPlatform _finalPlatform = null;
    [SerializeField] private SlowMoDetection _slowMoDetection = null;
    [SerializeField] private CharacterRoomsController _characterRoomsController = null;
    [SerializeField] private LevelCameraController _levelCameraController = null;
    [SerializeField] private KillZone _killZone = null;

    private void OnEnable()
    {
        Subscribe();
    }

    private void OnDisable()
    {
        UnSubscribe();
    }

    private void Awake()
    {
        InitializeLevelData();

        _characterRoomsController.ActiveRoom(0);
    }

    private void InitializeLevelData()
    {
        _levelCameraController.Initialize();
    }

    private void OnSlowMoActive()
    {
        _levelCameraController.SetTransformOnSlowMo();

        onSlowMoActive?.Invoke();
    }

    private void SetupCharacter(BaseCharacterController characterController)
    {
        characterController.SetLastMovePathPoint(_finalPlatform.MovePlaceTransform);

        _levelCharactersController.FillCharacterInfo(characterController);
    }

    private void OnCharacterCompletePath(BaseCharacterController characterController,Action callback)
    {
        Transform finalTransform = null;

        if(characterController.GetCharacterTeam == ECharacterTeam.PLAYER)
        {
            finalTransform = _finalPlatform.PlayerJumpPlace;
        }
        else if (characterController.GetCharacterTeam == ECharacterTeam.ENEMY)
        {
            finalTransform = _finalPlatform.EnemyJumpPlaceTransform;
        }
        characterController.DoMoveAfterPathCompleted(finalTransform, callback);
    }

    private void OnCharacterDetectFinalTrigger()
    {
        onPathCompleted?.Invoke(OnComplete);

        void OnComplete()
        {
            _levelCameraController.SetTransformOnFinal();
        }
    }

    private void OnLevelCompleted(ELevelCompleteReason levelCompleteReason)
    {
        onLevelCompleted?.Invoke(levelCompleteReason);
    }

    private void OnCharacterKilled(BaseCharacterController characterController)
    {
        _levelCharactersController.OnCharacterDie(characterController);
    }

    private void Subscribe()
    {
        _characterRoomsController.onCreateCharacter += SetupCharacter;
        _levelCharactersController.onCharacterCompletePath += OnCharacterCompletePath;

        _slowMoDetection.onCharacterEnter += OnSlowMoActive;

        _finalPlatform.onDetectCharacter += OnCharacterDetectFinalTrigger;

        _levelCharactersController.onLevelCompleted += OnLevelCompleted;

        _killZone.onDetectCharacter += OnCharacterKilled;
    }
    private void UnSubscribe()
    {
        _characterRoomsController.onCreateCharacter -= SetupCharacter;
        _levelCharactersController.onCharacterCompletePath -= OnCharacterCompletePath;

        _slowMoDetection.onCharacterEnter -= OnSlowMoActive;

        _finalPlatform.onDetectCharacter -= OnCharacterDetectFinalTrigger;

        _levelCharactersController.onLevelCompleted -= OnLevelCompleted;
        
        _killZone.onDetectCharacter -= OnCharacterKilled;
    }
}
