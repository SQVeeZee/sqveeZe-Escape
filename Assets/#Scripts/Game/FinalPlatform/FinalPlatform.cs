using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinalPlatform : MonoBehaviour
{
    [SerializeField] private Transform _jumpPlaceTransform = null;
    [SerializeField] private Transform _movePlaceTransform = null;

    public Transform MovePlaceTransform => _movePlaceTransform;
    public Transform JumpPlaceTransform => _jumpPlaceTransform;
}
