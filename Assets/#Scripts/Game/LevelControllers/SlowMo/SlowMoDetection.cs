using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlowMoDetection : MonoBehaviour
{
    public event Action onCharacterEnter = null;

    [SerializeField] private Collider _collider = null;

    private void OnTriggerEnter(Collider other)
    {
        BaseCharacterController characterController = other.GetComponent<BaseCharacterController>();

        if (characterController != null)
        {
            _collider.enabled = false;

            onCharacterEnter?.Invoke();
        }
    }
}
