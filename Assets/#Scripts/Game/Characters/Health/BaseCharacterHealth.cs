using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseCharacterHealth : MonoBehaviour
{
    public event Action onCharacterDie = null;

    private bool _isAlive = true;

    public bool IsAlive
    {
        get
        {
            return _isAlive;
        }
        set
        {
            _isAlive = value;
        }
    }

    public void KillCharacter()
    {
        onCharacterDie?.Invoke();
    }
}
