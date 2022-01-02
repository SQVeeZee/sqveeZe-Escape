using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseLevelItem : MonoBehaviour
{
    [SerializeField] List<BaseCharacterController> _baseCharacterControllers = null;
    [SerializeField] List<PathLineController> PathLineControllers = new List<PathLineController>();

    private void Awake()
    {
        SetStartTargets();
    }

    private void OnEnable()
    {
        foreach(BaseCharacterController characterController in _baseCharacterControllers)
        {
            characterController.onReachPoint += SetCharacterTarget;
        }
    }

    private void OnDisable()
    {
        foreach (BaseCharacterController characterController in _baseCharacterControllers)
        {
            characterController.onReachPoint -= SetCharacterTarget;
        }
    }

    protected void SetStartTargets()
    {
        for (int i = 0; i < _baseCharacterControllers.Count; i++)
        {
            SetCharacterTarget(_baseCharacterControllers[i], 0);
        }
    }

    private void SetCharacterTarget(BaseCharacterController characterController, int targetTransformId)
    {
        characterController.SetTargetTransform(PathLineControllers[targetTransformId].LinePoints
            [UnityEngine.Random.Range(0, PathLineControllers[targetTransformId].LinePoints.Count)]);
    }
}
