using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemiesRoomOut : BaseCharactersRoomOut
{
    [Header("Detection")]
    [SerializeField] private CharacterRoomDetection _characterRoomDetection = null;

    private void OnEnable()
    {
        _characterRoomDetection.onDetectPlayer += ActivateRoom;
    }

    private void OnDisable()
    {
        _characterRoomDetection.onDetectPlayer -= ActivateRoom;
    }

    public override void ActivateRoom()
    {
        _characterRoomDetection.SetColiderState(false);

        base.ActivateRoom();
    }
}
