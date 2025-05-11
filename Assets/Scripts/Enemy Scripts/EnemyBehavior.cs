using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Pool;

public class EnemyBehavior : MonoBehaviour
{

    public GameObject SetTarget { set { _target = value; } }

    private GameObject _target;

    private NavMeshAgent _navMesh;

    private ObjectPool<EnemyBehavior> _pool;

    public ObjectPool<EnemyBehavior> Pool { set { _pool = value; } } 

    private void Awake()
    {
        _navMesh = GetComponent<NavMeshAgent>();
        
    }

    private void Start()
    {
        if (!_navMesh)
        {
            Debug.LogError("EnemyBehavior: No instance of NavMeshAgent Component!");
            return;
        }

    }

    private void Update()
    {
        if(_target != null)
        {
            _navMesh.SetDestination(_target.transform.position);
        }
    }

}
