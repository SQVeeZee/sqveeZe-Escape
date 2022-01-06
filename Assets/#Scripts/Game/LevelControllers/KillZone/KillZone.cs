using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KillZone : MonoBehaviour
{
    public event Action<BaseCharacterController> onDetectCharacter;

    private void OnTriggerEnter(Collider other)
    {
        BaseCharacterController _baseCharacterController = other.GetComponent<BaseCharacterController>();

        if (_baseCharacterController != null)
        {
            onDetectCharacter?.Invoke(_baseCharacterController);
        }
    }
}
