using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public abstract class BaseLevelItem : MonoBehaviour, ILevelItem
{
    public event Action onPathCompleted;
    public event Action onSlowMoActive;
    public event Action<ELevelCompleteReason> onLevelCompleted;

    [Header("Rooms")]
    [SerializeField] private List<CharactersRoomOut> _ñharactersRoomOut = new List<CharactersRoomOut>();

    [Header("Final Platform")]
    [SerializeField] private FinalPlatform _finalPlatform = null;

    [Header("SlowMo")]
    [SerializeField] private SlowMoDetection _slowMoDetection = null;

    [SerializeField] private List<Transform> _cameraPoints = null;
    [SerializeField] private Transform _slowMoCameraPosition = null;
    [SerializeField] private Transform _finalCameraPosition = null;

    private List<BaseCharacterController> _aliveCharacterControllers = new List<BaseCharacterController>();
    private List<BaseCharacterController> _charactersFinished = new List<BaseCharacterController>();

    private CameraItem _gameCameraItem = null;

    private void OnEnable()
    {
        foreach(CharactersRoomOut roomOut in _ñharactersRoomOut)
        {
            roomOut.onCreateCharacter += SetupCharacter;
        }
        _slowMoDetection.onCharacterEnter += OnSlowMoActive;
    }

    private void OnDisable()
    {
        foreach (CharactersRoomOut roomOut in _ñharactersRoomOut)
        {
            roomOut.onCreateCharacter -= SetupCharacter;
        }
        _slowMoDetection.onCharacterEnter -= OnSlowMoActive;
    }

    private void Awake()
    {
        _ñharactersRoomOut[0].ActivateRoom();

        _gameCameraItem = (CameraItem)CameraManager.Instance.GetCameraItem(ECameraType.GAME);

        _gameCameraItem.MoveOnPath(_cameraPoints);
    }

    private void OnSlowMoActive()
    {
        onSlowMoActive?.Invoke();

        _gameCameraItem.ChangePosition(_slowMoCameraPosition);
    }

    private void SetupCharacter(BaseCharacterController characterController)
    {
        if (characterController.GetCharacterTeam == ECharacterTeam.PLAYER)
        {
            _aliveCharacterControllers.Add(characterController);
            characterController.onCharacterFinishPath += IsAllCharactersCompletePath;
        }
        characterController.onCharacterDie += OnCharacterDie;
    }

    private void IsAllCharactersCompletePath(BaseCharacterController finishedCharacter)
    {
        _charactersFinished.Add(finishedCharacter);

        if (IsAllCharactersFinished())
        {
            Do();

            _gameCameraItem.ChangePosition(_finalCameraPosition, delegate { onPathCompleted?.Invoke(); });
        }
    }

    private void OnCharacterDie(BaseCharacterController characterController)
    {
        if(characterController.GetCharacterTeam == ECharacterTeam.PLAYER)
        {
            _aliveCharacterControllers.Remove(characterController);

            if (_aliveCharacterControllers.Count == 0)
            {
                onLevelCompleted?.Invoke(ELevelCompleteReason.LOSE);
            }
        }

        characterController.gameObject.SetActive(false);
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
