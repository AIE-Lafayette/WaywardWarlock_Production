using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.AI;

public class EnemyAnimationStateController : MonoBehaviour
{
    Animator _animator;
    NavMeshAgent _agent;
    HealthComponent _healthComponent;
    EnemyBehavior _enemyBehavior;
    Collider _collider;

    // Start is called before the first frame update
    void Start()
    {
        _collider = GetComponentInChildren<Collider>();
        _enemyBehavior = GetComponent<EnemyBehavior>();
        _animator = GetComponentInChildren<Animator>();
        _agent = GetComponent<NavMeshAgent>();
        _healthComponent = GetComponent<HealthComponent>();
        _animator.SetFloat("Speed", _agent.speed);
    }

    // Update is called once per frame
    void Update()
    {
        AnimationState();
    }

    void AnimationState()
    {
        _animator.SetFloat("Speed", _agent.speed);
        if (_animator.GetBool("Attack") == true)
            _animator.SetTrigger("Attack");

        if (_healthComponent.Health == 0)
        {
            if (_animator.GetBool("IdleWalk") == true)
                _animator.SetBool("IdleWalk", false);
            if (_animator.GetBool("Death") == false)
                _animator.SetBool("Death", true);
            _agent.isStopped = true;
            _collider.enabled = false;

            if (_animator.GetCurrentAnimatorStateInfo(0).IsName("GolemDeath") && _animator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1.0f)
            {
                _collider.enabled = true;
                _enemyBehavior.Death();

            }
        }
        else
        {
            if (_animator.GetBool("IdleWalk") == false)
                _animator.SetBool("IdleWalk", true);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
       if(collision.gameObject.tag == "Player")
        {
            if(_animator.GetBool("Attack") == false)
                _animator.SetTrigger("Attack");
        }
    }
}
