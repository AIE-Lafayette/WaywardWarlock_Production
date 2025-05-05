using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Pool;

public class EnemyBehavior : MonoBehaviour
{

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
            Debug.LogError("EnemyBehaviour: No instance of NavMeshAgent Component!");
            return;
        }
    }

    
    public void SetDestination(GameObject obj)
    {
        _navMesh.SetDestination(obj.transform.position);
    }

}
