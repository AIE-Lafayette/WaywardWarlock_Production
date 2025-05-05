using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class SpawnPoint : MonoBehaviour
{

    private void OnDrawGizmos()
    {
        Gizmos.DrawSphere(transform.position,1.5f);
    }
    public void TestSpawn()
    {
        Spawn(1);
    }
    public void Spawn(int type)
    {
        switch(type)
        {
            case 1:
            {

                EnemyBehavior basegolem = EnemyPooler.instance.BaseGolemPool.Get();
                basegolem.transform.position = transform.position;
                break;
            }

            case 2:
            {

                EnemyBehavior icegolem = EnemyPooler.instance.IceGolemPool.Get();
                icegolem.transform.position = transform.position;
                break;
            }
            case 3:
            {
                EnemyBehavior firegolem = EnemyPooler.instance.FireGolemPool.Get();
                firegolem.transform.position = transform.position;
                break;
            }
            case 4:
            {

                EnemyBehavior lightninggolem = EnemyPooler.instance.LightningGolemPool.Get();
                lightninggolem.transform.position = transform.position;
                break;
            }
        }

    }

   
    

}
