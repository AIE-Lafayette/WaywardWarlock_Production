using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{
    Animator _animator;
    HealthComponent _healthComponent;
   
    public Vector3 _velocity;
    public Vector3 _previousPosition;

    void Start()
    {
        _animator = GetComponentInChildren<Animator>();
        _healthComponent = GetComponent<HealthComponent>();
        _animator.SetFloat("Speed", 0f);
        _animator.SetBool("IdleWalk", true);
    }

    void Update()
    {
        Animation();   
    }

    void Animation()
    {
        Vector3 _newVelocity = (transform.position - _previousPosition) / Time.deltaTime;
        _velocity = Vector3.Lerp(_velocity, _newVelocity, 0.015f); 

        _previousPosition = transform.position;

        float _speed = _velocity.magnitude;
        _animator.SetFloat("Speed", _speed);

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
