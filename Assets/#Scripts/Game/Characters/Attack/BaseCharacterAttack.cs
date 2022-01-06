using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseCharacterAttack : MonoBehaviour
{
    [SerializeField] protected Collider _attackCollider = null;

    protected abstract void OnTriggerEnter(Collider other);

    public void SetAttackColliderState(bool state)
    {
        _attackCollider.enabled = state;
    }
}