using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{
    Animator _animator;
    HealthComponent _healthComponent;
    CharacterController _controller;
    void Start()
    {
        _animator = GetComponentInChildren<Animator>();
        _healthComponent = GetComponent<HealthComponent>();
        _controller = GetComponent<CharacterController>();
        _animator.SetFloat("Speed", _controller.velocity.normalized.magnitude);
    }

    void Update()
    {
        Animation();   
    }

    void Animation()
    {
    
         
        

        if (_healthComponent.Health <= 0)
        {
            if (_animator.GetBool("IdleWalk") == true)
            {
                _animator.SetBool("IdleWalk", false);
            }
            if (_animator.GetBool("ForbiddenSpell") == true)
            {
                _animator.SetBool("ForbiddenSpell", false);
            }
            _animator.SetBool("WeyDeath", true);
        }
        else
        {
            if(_animator.GetCurrentAnimatorStateInfo(0).IsName("ForbiddenSpell") && _animator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1.0f )
            {
                _animator.SetBool("IdleWalk", true);
            }
        }
        


    }

    public void SetSpecial()
    {
        _animator.SetBool("ForbiddenSpell", true);
    }
}
