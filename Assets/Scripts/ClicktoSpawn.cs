using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClicktoSpawn : MonoBehaviour
{

    public SpawnPoint spawner;
    private void Update()
    {
        if(Input.GetMouseButton(0))
        {
            spawner.TestSpawn();
        }
    }
}
