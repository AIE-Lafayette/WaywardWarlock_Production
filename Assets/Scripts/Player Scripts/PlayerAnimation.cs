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
        
    }

    void Animation()
    {
        _animator.SetFloat("Speed", 1f);

    }
}
