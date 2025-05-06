using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class SpawnPoint : MonoBehaviour
{
    private EnemyBehavior golem;
    private void OnDrawGizmos()
    {
        Gizmos.DrawSphere(transform.position,1.5f);
    }

    public void Spawn(int type)
    {
        switch(type)
        {
            
            case 1:
            {
                golem = EnemyPooler.instance.BaseGolemPool.Get();
                golem.gameObject.transform.localPosition = gameObject.transform.localPosition;
                break;
            }

            case 2:
            {

                golem = EnemyPooler.instance.IceGolemPool.Get();
                golem.gameObject.transform.localPosition = gameObject.transform.localPosition;
                break;
            }
            case 3:
            {
                golem = EnemyPooler.instance.FireGolemPool.Get();
                golem.gameObject.transform.localPosition = transform.localPosition;
                break;
            }
            case 4:
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
