using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class SpawnPointManager : MonoBehaviour, IComparer<SpawnPoint>
{
    [SerializeField]
    private List<SpawnPoint> _spawnPointList;

    [SerializeField]
    GameObject _player;

    [SerializeField]
    private int _spawnPointLimit = 3;

    [SerializeField]
    float _timeDelay = 10.0f;

    public Queue<EnemyType> SetSpawnList 
    { set 
        { 
            if(IsDoneSpawning)
            {
                _spawnList = value; 
                _isDoneSpawning = false;
                
            }
        } 
    }
    public bool StopSpawning
    {
        get
        {
            return _stopSpawning;
        }
        set
        {
            _stopSpawning = value;
            _timer = 0f;
        }
    }
    public bool IsDoneSpawning { get { return _isDoneSpawning; } }


    private Queue<EnemyType> _spawnList;

    float _timer = 0;
    bool _stopSpawning = false;
    bool _isDoneSpawning = true;

    private void Start()
    { 
        if (_spawnPointList.Count == 0)
            Debug.LogWarning("SpawnManager: No spawn points in the list!");

        if (_player == null)
            Debug.LogWarning("SpawnManager: No GameObject set!");
        
        
    }

    private void Update()
    {
        if (_stopSpawning || _isDoneSpawning)
            return;

        CheckPositions();

        _timer += Time.deltaTime;
        if(_timer >= _timeDelay)
        {
               _timer -= _timeDelay;
                SpawnEnemies(); 
        }
        
    }

    //Compares spawn points to check for smallest distance
    public int Compare(SpawnPoint a, SpawnPoint b)
    {
        if (a.DistanceToPlayer > b.DistanceToPlayer) return 1;
        else if (a.DistanceToPlayer < b.DistanceToPlayer) return -1;
        else return 0;
    }
    


    void CheckPositions()
    {
        foreach (SpawnPoint point in _spawnPointList)
        {
            point.DistanceToPlayer = Vector3.Distance(point.transform.position, _player.transform.position);
        }
        _spawnPointList.Sort(Compare);
    }
    
    void SpawnEnemies()
    {
        for (int i = 0; i < _spawnPointLimit; i++)
        {
            if(_spawnList.Count == 0)
            {
                _isDoneSpawning = true;
                break;
            }
            _spawnPointList[i].Spawn(_spawnList.Dequeue(), _player);
        }
    }
}

public enum EnemyType
{
    BASE = 0,
    ICE,
    FIRE,
    LIGHTNING
}
