using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinalCameraTrigger : BaseDetection
{
    public event Action onDetectPlayer = null;

    protected override void OnTriggerEnter(Collider other)
    {
        BaseCharacterController characterController = other.GetComponent<BaseCharacterController>();

        if (characterController != null && characterController.GetCharacterTeam == ECharacterTeam.PLAYER)
        {
            SetColiderState(false);

            onDetectPlayer?.Invoke();
        }
    }
}
