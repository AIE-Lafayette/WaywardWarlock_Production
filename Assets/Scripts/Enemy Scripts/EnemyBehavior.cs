using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyBehavior : MonoBehaviour
{

    private NavMeshAgent _navMesh;



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
