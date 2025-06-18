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

    private void OnDrawGizmos()
    {
        Gizmos.DrawSphere(transform.position,1.5f);
    }

    Vector3 GetNavMesh()
    {
        Ray ray = new Ray(transform.position, -transform.up);
        if (Physics.Raycast(ray, out RaycastHit rayhit, 60))
        {
            TerrainCollider _mesh = rayhit.collider.gameObject.GetComponent<TerrainCollider>();
            if (_mesh != null)
            {
                return rayhit.point;
            }
        }
        return transform.localPosition;
    }

    void SetEnemy(GameObject player)
    {
        _navMesh = golem.GetComponent<NavMeshAgent>();
        _navMesh.enabled = false;
        _navMesh.Warp(transform.position);
        _navMesh.enabled = true;
        golem.SetTarget = player;
        EnemyPooler.instance.ActiveList.Add(golem);
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
