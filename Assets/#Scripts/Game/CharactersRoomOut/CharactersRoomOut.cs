using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharactersRoomOut : MonoBehaviour
{
    public event Action<BaseCharacterController> onCreateCharacter = null;

    [SerializeField] private BaseCharacterController _characterPrefab = null;
    [SerializeField] private Transform _roomTransform = null;

    [Header("Path lines")]
    [SerializeField] private PathLineController _pathLineSpawn = null;
    [SerializeField] private List<PathLineController> _pathLineControllers = new List<PathLineController>();

    [Header("Detection")]
    [SerializeField] private CharacterRoomDetection _characterRoomDetection = null;

    [Header("Settings")]
    [SerializeField] private int _minCharacterCount = 5;
    [SerializeField] private int _maxCharacterCount = 10;
    [SerializeField] private float _spawnDelay = 0.3f;
    [SerializeField] private float _startSpawnDelay = 1f;

    private int _characterId = 0;
    private int _spawnId = 0;

    private void OnEnable()
    {
        _characterRoomDetection.onDetectPlayer += ActivateRoom;
    }

    private void OnDisable()
    {
        _characterRoomDetection.onDetectPlayer -= ActivateRoom;
    }

    public void ActivateRoom()
    {
        _characterRoomDetection.SetColiderState(false);

        StartCoroutine(InstantiateWithDelay(_roomTransform));
    }

    private IEnumerator InstantiateWithDelay(Transform characterRoot)
    {
        WaitForSeconds timer = new WaitForSeconds(_spawnDelay);
        WaitForSeconds startTimer = new WaitForSeconds(_startSpawnDelay);

        int charactersCount = GetSpawnCharactersCount();

        yield return startTimer;
        
        for (int i = 0; i < charactersCount; i++)
        {
            yield return timer;

            InstantiateCharacter(characterRoot);
        }
    }

    private void InstantiateCharacter(Transform characterRoot)
    {
        BaseCharacterController characterController = Instantiate(_characterPrefab, GetSpawnPosition(), Quaternion.identity);

        characterController.transform.SetParent(characterRoot);

        characterController.Initialize(GetMovePath());

        onCreateCharacter?.Invoke(characterController);
    }

    private List<Vector3> GetMovePath()
    {
        List<Vector3> movePath = new List<Vector3>();

        foreach (PathLineController pathLine in _pathLineControllers)
        {
            movePath.Add(pathLine.GetUniquePoint(_characterId));
        }
        _characterId++;

        return movePath;
    }

    private int GetSpawnCharactersCount()
    {
        return UnityEngine.Random.Range(_minCharacterCount, _maxCharacterCount);
    }

    private Vector3 GetSpawnPosition()
    {
        var a = _pathLineSpawn.GetUniquePoint(_spawnId);
        
        _spawnId++;

        return a;
    }
}
