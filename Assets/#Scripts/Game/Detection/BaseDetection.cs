using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseDetection : MonoBehaviour
{
    [SerializeField] protected Collider _collider = null;

    protected virtual void OnTriggerEnter(Collider other)
    {

    }

    public void SetColiderState(bool state)
    {
        _collider.enabled = state;
    }
}
