using System.Collections;
using System.Collections.Generic;
<<<<<<< HEAD
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Pool;
=======
using UnityEditor;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Events;
using UnityEngine.Pool;
using UnityEngine.VFX;
>>>>>>> parent of 055ffb5 (Revert "Merge branch 'Dev' into Jack")

public class EnemyBehavior : MonoBehaviour
{
    [SerializeField]
    private float _damage = 1;
<<<<<<< HEAD


    public GameObject SetTarget { set { _target = value; } }
    public ObjectPool<EnemyBehavior> Pool { set { _pool = value; } }


=======
    [SerializeField]
    private GameObject _itemDrop;
    [SerializeField]
    private VisualEffect _forbiddenSpellEffect;

    public UnityEvent OnEnemyDeath;
    public GameObject SetTarget { set { _target = value; } }
    public ObjectPool<EnemyBehavior> Pool { set { _pool = value; } }
    public bool IsKilled { get { return _killed; } set { _killed = value; } }
    
>>>>>>> parent of 055ffb5 (Revert "Merge branch 'Dev' into Jack")
    private HealthComponent _health;
    private GameObject _target;
    private NavMeshAgent _navMesh;
    private ObjectPool<EnemyBehavior> _pool;
<<<<<<< HEAD
=======
    private bool _killed = false;
>>>>>>> parent of 055ffb5 (Revert "Merge branch 'Dev' into Jack")

    private float _timer;
    private float _delay = 1.5f;

<<<<<<< HEAD
=======
    public bool StopMovement { set { _navMesh.isStopped = value; } }
>>>>>>> parent of 055ffb5 (Revert "Merge branch 'Dev' into Jack")

    private void Awake()
    {
        _navMesh = GetComponent<NavMeshAgent>();
        _health = GetComponent<HealthComponent>();
<<<<<<< HEAD
=======
        
>>>>>>> parent of 055ffb5 (Revert "Merge branch 'Dev' into Jack")
    }

    private void Start()
    {
<<<<<<< HEAD
=======
        
>>>>>>> parent of 055ffb5 (Revert "Merge branch 'Dev' into Jack")
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
<<<<<<< HEAD
            Debug.LogError("EnemyBehavior: Object Pool is null!");
            return;
        }
=======
            Debug.LogWarning("EnemyBehavior: Object Pool is null!");
            return;
        }
        
>>>>>>> parent of 055ffb5 (Revert "Merge branch 'Dev' into Jack")
    }

    private void Update()
    {
        if(_target != null)
        {
            _navMesh.SetDestination(_target.transform.position);
<<<<<<< HEAD
        }


        if(_health.Health == 0)
        {
            OnDeath();
        }
=======
            if(_health.Health != 0)
            {
                _navMesh.transform.LookAt(_target.transform);
            }
        }
    }
    public void Death()
    {
        if (_pool != null)
        {

            _killed = true;
            GameManager.instance.AddKill();
            EnemyPooler.instance.ActiveList.Remove(this);
            OnEnemyDeath.Invoke();
        }
        else
        {
            DropItem();
            EnemyPooler.instance.ActiveList.Remove(this);
            Destroy(gameObject);
        }
    }

    public void SpecialDeath()
    {
        _navMesh.isStopped = true;
        Instantiate(_forbiddenSpellEffect,transform.position,Quaternion.identity);
        OnEnemyDeath.Invoke();
>>>>>>> parent of 055ffb5 (Revert "Merge branch 'Dev' into Jack")

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
<<<<<<< HEAD
        _timer += Time.deltaTime;
        if(_timer > _delay)
        {
            _timer -= _delay;
            HitPlayer(collision);
        }
    }

    void OnDeath()
    {
        _health.ResetHealth();
        GameManager.instance.KillCount += 1;
=======
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


    public void DropItem()
    {
        if(_itemDrop)
        {
            Instantiate(_itemDrop, transform.position, Quaternion.identity);
        }

    }

    public void Return()
    {
        _navMesh.isStopped = false;
>>>>>>> parent of 055ffb5 (Revert "Merge branch 'Dev' into Jack")
        _pool.Release(this);
    }
}
