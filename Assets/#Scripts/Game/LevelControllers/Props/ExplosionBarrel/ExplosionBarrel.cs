using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosionBarrel : MonoBehaviour
{
    [SerializeField] private GameObject _barrelModel = null;

    [SerializeField] private Collider _barrelCollider = null;
    [SerializeField] private BarrelDamageCollider _barrelDamageCollider = null;

    private void Awake()
    {
        _barrelDamageCollider.SetColliderState(false);

        SetColliderState(true);
    }

    public void BlowUpBarrel()
    {
        _barrelModel.SetActive(false);

        SetColliderState(false);

        _barrelDamageCollider.SetColliderState(true);

        StartCoroutine(ResetBlowUp());
    }

    private IEnumerator ResetBlowUp()
    {
        yield return new WaitForSeconds(0.1f);

        _barrelDamageCollider.SetColliderState(false);
    }

    private void SetColliderState(bool state)
    {
        _barrelCollider.enabled = state;
    }
}
