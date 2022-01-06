using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinalPlatform : MonoBehaviour
{
    public event Action onDetectCharacter = null;

    [Header("Camera")]
    [SerializeField] private FinalCameraTrigger _finalCameraTrigger = null;
    [SerializeField] private Transform _slowMoCameraPosition = null;
    [SerializeField] private Transform _finalCameraPosition = null;

    [Header("Transforms")]
    [SerializeField] private Transform _playerJumpPlaceTransform = null;
    [SerializeField] private Transform _enemyJumpPlaceTransform = null;
    [SerializeField] private Transform _movePlaceTransform = null;

    public Transform MovePlaceTransform => _movePlaceTransform;
    public Transform PlayerJumpPlace => _playerJumpPlaceTransform;
    public Transform EnemyJumpPlaceTransform => _enemyJumpPlaceTransform;

    public Transform GetSlowMoCameraTransform => _slowMoCameraPosition;
    public Transform GetFinalCameraTransform => _finalCameraPosition;

    private void OnEnable()
    {
        _finalCameraTrigger.onDetectPlayer += delegate { onDetectCharacter?.Invoke(); };
    }

    private void OnDisable()
    {
        _finalCameraTrigger.onDetectPlayer -= delegate { onDetectCharacter?.Invoke(); };
    }
}
