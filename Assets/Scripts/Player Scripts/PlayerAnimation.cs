using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{
    Animator _animator;
    HealthComponent _healthComponent;
    void Start()
    {
        _animator = GetComponentInChildren<Animator>();
        _healthComponent = GetComponent<HealthComponent>();
        _animator.SetFloat("Speed", 1f);
    }

    void Update()
    {
        Animation();   
    }

    void Animation()
    {
        _animator.SetFloat("Speed", 1f);

       if(_healthComponent.Health == 0 )
        {
            if (_animator.GetBool("IdleWalk") == true)
            {
              _animator.SetBool("IdleWalk", false);
            }
            if (_animator.GetBool("ForbiddenSpell") == true)
            {
                _animator.SetBool("ForbiddenSpell", false);
            }
            else
            {
                if (_animator.GetBool("IdleWalk") == false)
                {
                    _animator.SetBool("IdleWalk", true);
                }
            }
        }


    }
}
