using UnityEngine;

public class CharacterAnimatorBase : MonoBehaviour
{
    private static readonly int IsRunParameterName = Animator.StringToHash("IsRun");
    private static readonly int IsAttackParameterName = Animator.StringToHash("IsAttack");
    private static readonly int Idle = Animator.StringToHash("Idle");

    [SerializeField] private Animator _animator;

    private bool _isRun;

    public Animator Animator => _animator;
    public bool IsRun
    {
        get => _isRun;
        set
        {
            if (value != _isRun)
            {
                _isRun = value;

                SetBool(IsRunParameterName, _isRun);
            }
        }
    }

    public void PlayIdle()
    {
        SetTrigger(Idle);
    }

    protected void SetBool(int id, bool value)
    {
        if (_animator == null)
        {
            return;
        }

        _animator.SetBool(id, value);
    }

    protected void SetTrigger(int id)
    {
        if (_animator == null)
        {
            return;
        }

        _animator.SetTrigger(id);
    }

    protected void SetFloat(int id, float value)
    {
        if (_animator == null)
        {
            return;
        }

        _animator.SetFloat(id, value);
    }
}
