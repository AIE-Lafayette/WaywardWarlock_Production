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

    float _delay = 3;
    float _timer = 0;
    // Start is called before the first frame update
    void Start()
    {
        _collider = GetComponent<Collider>();
        _enemyBehavior = GetComponent<EnemyBehavior>();
        _animator = GetComponentInChildren<Animator>();
        _agent = GetComponent<NavMeshAgent>();
        _healthComponent = GetComponent<HealthComponent>();
        _animator.SetFloat("Speed", _agent.speed);
    }

    // Update is called once per frame
    void Update()
    {
        if (_animator.GetBool("Attack") == true)
            _animator.SetTrigger("Attack");
        
        if(_healthComponent.Health == 0)
        {
            _timer += Time.deltaTime;
            if(_animator.GetBool("IdleWalk") == true)
                _animator.SetTrigger("IdleWalk");
            if(_animator.GetBool("Death") == false)
                _animator.SetTrigger("Death");
            _agent.isStopped = true;
            _collider.enabled = false;
            if(_timer >= _delay)
            {
                _collider.enabled = true;
                _enemyBehavior.Death();
            }
        }
        else
        {
            if (_animator.GetBool("IdleWalk") == false)
                _animator.SetTrigger("IdleWalk");
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
