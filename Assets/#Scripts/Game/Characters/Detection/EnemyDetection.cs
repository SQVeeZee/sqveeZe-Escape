using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDetection : BaseCharacterDetection
{
    protected override void OnTriggerEnter(Collider other)
    {
        base.OnTriggerEnter(other);

        BaseCharacterController characterController = other.GetComponent<BaseCharacterController>();

        if(characterController != null && characterController.GetCharacterTeam == ECharacterTeam.PLAYER)
        {
            characterController.BaseCharacterHealth.KillCharacter();
        }
    }
}
