using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterRoomsController : MonoBehaviour
{
    public event Action<BaseCharacterController> onCreateCharacter = null;

    [Header("Rooms")]
    [SerializeField] private List<BaseCharactersRoomOut> _charactersRoomOut = new List<BaseCharactersRoomOut>();

    private void OnEnable()
    {
        foreach (BaseCharactersRoomOut roomOut in _charactersRoomOut)
        {
            roomOut.onCreateCharacter += OnCreateCharacter;
        }
    }

    private void OnDisable()
    {
        foreach (BaseCharactersRoomOut roomOut in _charactersRoomOut)
        {
            roomOut.onCreateCharacter += OnCreateCharacter;
        }
    }

    private void OnCreateCharacter(BaseCharacterController characterController)
    {
        onCreateCharacter?.Invoke(characterController);
    }

    public void ActiveRoom(int roomId)
    {
        _charactersRoomOut[roomId].ActivateRoom();
    }
}
