using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseDetection : MonoBehaviour,IDetection
{
    [SerializeField] private Collider _collider = null;
    
    public void SetColiderState(bool state)
    {
        _collider.enabled = state;
    }

    protected abstract void OnTriggerEnter(Collider other);
}
