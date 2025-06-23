using System.Collections;
using System.Collections.Generic;
using Unity.AI.Navigation;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;


public class SpawnPoint : MonoBehaviour
{
    private EnemyBehavior golem;
    private NavMeshAgent _navMesh;
    public float DistanceToPlayer { get { return _distanceToPlayer; } set { _distanceToPlayer = value; } }

    private float _distanceToPlayer;
    float _maxDistance = 10;
    Vector3 _spawnPosition;

    private void OnDrawGizmos()
    {
        Gizmos.DrawSphere(transform.position,1.5f);
    }
  

    void SetEnemy(GameObject player)
    {
        _navMesh = golem.GetComponent<NavMeshAgent>();
        NavMeshHit hit;
        if (NavMesh.SamplePosition(transform.position, out hit, _maxDistance, NavMesh.AllAreas))
        {
            _spawnPosition = hit.position;
            _navMesh.enabled = false;
            _navMesh.Warp(_spawnPosition);
            _navMesh.enabled = true;
            golem.SetTarget = player;
            EnemyPooler.instance.ActiveList.Add(golem);
        }
       
    }

    public void Spawn(EnemyType type,GameObject player)
    {
        switch(type)
        {
            case EnemyType.BASE:
            {
                    golem = EnemyPooler.instance.BaseGolemPool.Get();
                    SetEnemy(player);
                    break;
            }

            case EnemyType.ICE:
            {

                    golem = EnemyPooler.instance.IceGolemPool.Get();
                    SetEnemy(player);
                    break;
            }
            case EnemyType.FIRE:
            {
                    golem = EnemyPooler.instance.FireGolemPool.Get();
                    SetEnemy(player);
                    break;
            }
            case EnemyType.LIGHTNING:
            {

                    golem = EnemyPooler.instance.LightningGolemPool.Get();
                    SetEnemy(player);
                    break;
            }
            default:
                    golem = EnemyPooler.instance.BaseGolemPool.Get();
                    SetEnemy(player);
                    break;
        }

    }

   
    

}
