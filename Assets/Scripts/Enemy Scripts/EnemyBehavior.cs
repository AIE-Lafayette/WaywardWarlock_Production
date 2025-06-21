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
    private float _turnSpeed =10f;
    [SerializeField]
    private bool _isLightningGolem;

    Coroutine _updateTarget;

    Vector3 _placementOffset;

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
        StartCoroutine(UpdateTarget());
        _placementOffset = new Vector3(0, 1, 0);
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
    IEnumerator UpdateTarget()
    {
        while(_health.Health != 0)
        {
            _navMesh.SetDestination(_target.transform.position);
            yield return new WaitForSeconds(.5f);
        }
    }
    private void Update()
    {
        if(_target != null)
        {
            
            if(_health.Health != 0)
            {

                Vector3 flatVelocity = new Vector3(_navMesh.velocity.x, 0f, _navMesh.velocity.z);
                if(flatVelocity.sqrMagnitude > 0.001f)
                {
                    Quaternion lookRotation = Quaternion.LookRotation(flatVelocity);
                    transform.GetChild(0).rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * _turnSpeed);
                }
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
            Ray ray = new Ray(transform.position, -transform.up);
            if(Physics.Raycast(ray, out RaycastHit hit, 10))
            {
                Collider _ground = hit.collider.gameObject.GetComponent<Collider>();
                if(_ground != null)
                {
                    Instantiate(_itemDrop, hit.point + _placementOffset, Quaternion.identity);

                }
            }
            else
                Instantiate(_itemDrop, transform.position, Quaternion.identity);
        }
    }

    public void Return()
    {
        _navMesh.isStopped = false;
        _pool.Release(this);
    }
}
