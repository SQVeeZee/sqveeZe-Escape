using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttack : BaseCharacterAttack
{
    protected override void OnTriggerEnter(Collider other)
    {
        BaseCharacterController characterController = other.GetComponent<BaseCharacterController>();

        if (characterController != null && characterController.GetCharacterTeam == ECharacterTeam.PLAYER)
        {
            characterController.BaseCharacterHealth.KillCharacter();
        }
    }
}
