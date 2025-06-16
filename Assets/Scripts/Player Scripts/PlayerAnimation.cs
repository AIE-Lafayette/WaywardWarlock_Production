using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{
    Animator _animator;
    HealthComponent _healthComponent;
    CharacterController _controller;
    public Vector3 _velocity;
    public Vector3 _previousPosition;

    void Start()
    {
        _animator = GetComponentInChildren<Animator>();
        _healthComponent = GetComponent<HealthComponent>();
        _controller = GetComponent<CharacterController>();
        _animator.SetFloat("Speed", 0f);
        _animator.SetBool("IdleWalk", true);
    }

    void Update()
    {
        Animation();   
    }

    void Animation()
    {
        _velocity = (transform.position - _previousPosition) / Time.deltaTime;

        _previousPosition = transform.position;
        Debug.Log(_velocity.normalized.magnitude);
        _animator.SetFloat("Speed", _velocity.normalized.magnitude);

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
