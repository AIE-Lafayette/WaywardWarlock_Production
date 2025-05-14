using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor.ShaderKeywordFilter;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    private SpawnPointManager _spawnManager;
    

    public int KillCount { get { return _killCount; } set { _killCount += Mathf.Abs(value); } }

    private Queue<EnemyType> _spawnList;
    public static GameManager instance;
    int _amountToSpawn = 100;
    int _maxEnemiesOnScreen = 50;
    int _killCount;
    float _basicGolemPercentage = .85f;
    int _specialTypes = 3;

    private void Start()
    {
        if (instance != null && instance != this)
        {
            Destroy(this);
        }
        else
        {
            instance = this;
        }

        _spawnList = new Queue<EnemyType>();
        if (_spawnManager == null)
        {
            Debug.LogWarning("GameManager: Spawn Point Manager is null!"); 
            return;
        }
        MakeQueue();

        _spawnManager.SetSpawnList = _spawnList;
       
    }


    private void Update()
    {
        CheckAmountEnemies();

    }

    void CheckAmountEnemies()
    {
        if (EnemyPooler.instance.AllActiveCount >= _maxEnemiesOnScreen)
        {
            if (_spawnManager.StopSpawning != true)
                _spawnManager.StopSpawning = true;
        }
        else
        {
            if (_spawnManager.StopSpawning != false)
                _spawnManager.StopSpawning = false;
        }
    }    
    void MakeQueue()
    {
       
        int basicGolems = (int)(_amountToSpawn * _basicGolemPercentage);
        int specialGolems = _amountToSpawn - basicGolems;

        int amoutPerSpecial = specialGolems / _specialTypes;
        int remainder = specialGolems % _specialTypes;

        List<EnemyType> tempList = new List<EnemyType>();

        //Add basic Enemys
        for(int i = 0; i < _amountToSpawn; i++)
        {
            tempList.Add(EnemyType.BASE);
        }
        //Add Special Enemies
        for (int i = 0; i < amoutPerSpecial; i++)
        {
            tempList.Add(EnemyType.FIRE);
            tempList.Add(EnemyType.ICE);
            tempList.Add(EnemyType.LIGHTNING);
        }
        //Add Remainder with Random Specials
        EnemyType[] specials = { EnemyType.ICE, EnemyType.FIRE, EnemyType.LIGHTNING };
        for (int i = 0; i < remainder; i++)
        {
            tempList.Add(specials[Random.Range(0, specials.Length)]);
        }

        ShuffleList<EnemyType>(tempList);

        //Do this so you never get a scecial golem on start
        for (int i = 0; i < 10; i++)
        {
            tempList.Insert(0, EnemyType.BASE);
        }

        //Add to the Queue
        foreach (EnemyType type in tempList)
        {
            _spawnList.Enqueue(type);
        }
    }

    void ShuffleList<T>(List<T> list)
    {
        for (int i = list.Count - 1; i > 0; i--)
        {
            int j = Random.Range(0, i + 1);
            T temp = list[i];
            list[i] = list[j];
            list[j] = temp;
        }
    }


}
