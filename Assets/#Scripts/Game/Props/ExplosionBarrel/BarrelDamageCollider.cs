using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BarrelDamageCollider : MonoBehaviour
{
    [SerializeField] private Collider _explosionCollider = null;

    private void OnTriggerEnter(Collider other)
    {
        BaseCharacterController characterController = other.GetComponent<BaseCharacterController>();

        if (characterController != null)
        {
            characterController.BaseCharacterHealth.KillCharacter();
        }
    }

    public void SetColliderState(bool state)
    {
        _explosionCollider.enabled = state;
    }
}
