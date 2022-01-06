using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterGroundCheck : MonoBehaviour
{
    [SerializeField] private LayerMask _groundMask = default;

    private RaycastHit[] m_Results = new RaycastHit[5];
    public bool IsGrounded()
    {
        bool isGrounded = true;

        int hits = Physics.RaycastNonAlloc(transform.position, -transform.up, m_Results, Mathf.Infinity, _groundMask);

        if (hits == 0) 
        {
            isGrounded = false;
        }

        return isGrounded;
    }
}
