using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Events;
using UnityEngine.Pool;
using UnityEngine.VFX;

public class EnemyBehavior : MonoBehaviour
{
    [SerializeField]
    private float _damage = 1;
    [SerializeField]
    private GameObject _itemDrop;
    [SerializeField]
    private VisualEffect _forbiddenSpellEffect;

    [SerializeField]
    private bool _isLightningGolem;

    public bool IsLightningGolem { get { return _isLightningGolem; } }

    public UnityEvent OnEnemyDeath;
    public GameObject SetTarget { set { _target = value; } }
    public ObjectPool<EnemyBehavior> Pool { set { _pool = value; } }
    public bool IsKilled { get { return _killed; } set { _killed = value; } }

    private HealthComponent _health;
    private GameObject _target;
    private NavMeshAgent _navMesh;
    private ObjectPool<EnemyBehavior> _pool;
    private bool _killed = false;

    private float _timer;
    private float _delay = 1.5f;

    public bool StopMovement { set { _navMesh.isStopped = value; } }

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
            Debug.LogWarning("EnemyBehavior: Object Pool is null!");
            return;
        }
        
    }

    private void Update()
    {
        if(_target != null)
        {
            _navMesh.SetDestination(_target.transform.position);
            if(_health.Health != 0)
            {
                Vector3 direction = _target.transform.position - transform.position;
                Quaternion targetRotation = Quaternion.LookRotation(direction);
                transform.GetChild(0).rotation = targetRotation;
               
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
        EffectsPool.instance.BeamPool.Get().transform.position = transform.position;
        OnEnemyDeath.Invoke();

    }
    void HitPlayer(Collider collision)
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

    private void OnTriggerEnter(Collider other)
    {
        HitPlayer(other);
    }

    private void OnTriggerStay(Collider other)
    {
        if (!_navMesh.isStopped)
        {
            _timer += Time.deltaTime;
            if (_timer > _delay)
            {
                _timer -= _delay;
                HitPlayer(other);
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
        _pool.Release(this);
    }
}
