using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Pool;

public class EnemyBehavior : MonoBehaviour
{
    [SerializeField]
    private float _damage = 1;


    public GameObject SetTarget { set { _target = value; } }
    public ObjectPool<EnemyBehavior> Pool { set { _pool = value; } }


    private HealthComponent _health;
    private GameObject _target;
    private NavMeshAgent _navMesh;
    private ObjectPool<EnemyBehavior> _pool;

    private float _timer;
    private float _delay = 1.5f;

    public bool CanMove { set { _navMesh.isStopped = value; } }

    private void Awake()
    {
        _navMesh = GetComponent<NavMeshAgent>();
        _health = GetComponent<HealthComponent>();
    }

    private void Start()
    {
        if (!_navMesh)
        {
            Debug.LogError("EnemyBehavior: No instance of NavMeshAgent Component!");
            return;
        }
        if(!_health)
        {
            Debug.LogError("EnemyBehavior: No health component!");
            return;
        }
        if(_pool == null)
        {
            Debug.LogError("EnemyBehavior: Object Pool is null!");
            return;
        }
    }

    private void Update()
    {
        if(_target != null)
        {
            _navMesh.SetDestination(_target.transform.position);
        }


        if(_health.Health == 0)
        {
            OnDeath();
        }

    }
    void HitPlayer(Collision collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            HealthComponent health = collision.gameObject.GetComponent<HealthComponent>();
            if (health != null)
            {
                health.RemoveHealth(_damage);
            }
        }

    }

    private void OnCollisionEnter(Collision collision)
    {
        HitPlayer(collision);
    }
    private void OnCollisionStay(Collision collision)
    {
        if(!_navMesh.isStopped)
        {
            _timer += Time.deltaTime;
            if(_timer > _delay)
            {
                _timer -= _delay;
                HitPlayer(collision);
            }
        }
    }

    void OnDeath()
    {
        _health.ResetHealth();
        GameManager.instance.KillCount += 1;
        _pool.Release(this);
    }
}
