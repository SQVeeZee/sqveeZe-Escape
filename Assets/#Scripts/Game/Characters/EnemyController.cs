using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : BaseCharacterController
{
    [SerializeField] private EnemyAttack _enemyAttack = null;

    public override void OnCharacterCompletePath()
    {
        base.OnCharacterCompletePath();

        _enemyAttack.SetAttackColliderState(false);
    }
}
