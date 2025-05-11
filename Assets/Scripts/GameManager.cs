using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    private SpawnPointManager _spawnManager;

    private Queue<EnemyType> _spawnList;

    private void Start()
    {
        _spawnList = new Queue<EnemyType>();
        if (_spawnManager == null)
        {
            Debug.LogWarning("GameManager: Spawn Point Manager is null!"); 
            return;
        }


       
        _spawnList.Enqueue(EnemyType.BASE);
        _spawnList.Enqueue(EnemyType.BASE);
        _spawnList.Enqueue(EnemyType.BASE);
        _spawnList.Enqueue(EnemyType.BASE);
        _spawnList.Enqueue(EnemyType.BASE);
        _spawnList.Enqueue(EnemyType.BASE);
        _spawnList.Enqueue(EnemyType.BASE);
        _spawnList.Enqueue(EnemyType.BASE);
        _spawnList.Enqueue(EnemyType.BASE);
        _spawnList.Enqueue(EnemyType.BASE);
        _spawnList.Enqueue(EnemyType.BASE);
        _spawnList.Enqueue(EnemyType.BASE);
        _spawnList.Enqueue(EnemyType.BASE);
        _spawnList.Enqueue(EnemyType.BASE);
        _spawnList.Enqueue(EnemyType.BASE);
        _spawnList.Enqueue(EnemyType.BASE);
        _spawnList.Enqueue(EnemyType.BASE);
        _spawnList.Enqueue(EnemyType.BASE);

        _spawnManager.SetSpawnList = _spawnList;
    }


    private void Update()
    {
        
    }





}
