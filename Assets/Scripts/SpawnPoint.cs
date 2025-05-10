using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class SpawnPoint : MonoBehaviour
{
    private EnemyBehavior golem;
    public float DistanceToPlayer { get { return _distanceToPlayer; } set { _distanceToPlayer = value; } }

    private float _distanceToPlayer;

    private void OnDrawGizmos()
    {
        Gizmos.DrawSphere(transform.position,1.5f);
    }

    public void Spawn(EnemyType type,Transform player)
    {
        switch(type)
        {
            
            case EnemyType.BASE:
            {
                golem = EnemyPooler.instance.BaseGolemPool.Get();
                golem.gameObject.transform.localPosition = gameObject.transform.localPosition;
                break;
            }

            case EnemyType.ICE:
            {

                golem = EnemyPooler.instance.IceGolemPool.Get();
                golem.gameObject.transform.localPosition = gameObject.transform.localPosition;
                break;
            }
            case EnemyType.FIRE:
            {
                golem = EnemyPooler.instance.FireGolemPool.Get();
                golem.gameObject.transform.localPosition = transform.localPosition;
                break;
            }
            case EnemyType.LIGHTNING:
            {

                golem = EnemyPooler.instance.LightningGolemPool.Get();
                golem.gameObject.transform.position = transform.localPosition;
                break;
            }
            default:
                golem = EnemyPooler.instance.BaseGolemPool.Get();
                golem.gameObject.transform.localPosition = gameObject.transform.localPosition;
                break;
        }

    }

   
    

}
