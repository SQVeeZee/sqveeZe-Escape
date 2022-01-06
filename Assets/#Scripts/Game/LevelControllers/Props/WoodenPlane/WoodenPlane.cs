using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WoodenPlane : MonoBehaviour
{
    [SerializeField] private Collider _collider = null;
    [SerializeField] private GameObject _model = null;

    public void BreakPlanes()
    {
        _collider.enabled = false;
        _model.SetActive(false);
    }
}
