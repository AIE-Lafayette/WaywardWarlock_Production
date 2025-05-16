using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor.ShaderKeywordFilter;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    private SpawnPointManager _spawnManager;
    [SerializeField]
    int _maxEnemiesOnScreen = 50;

    public int KillCount { get { return _killCount; } set { _killCount += Mathf.Abs(value); } }

    private Queue<EnemyType> _spawnList;
    public static GameManager instance;
    int _incrementAmount = 5;
    int _amountToSpawn = 10;
    int _killCount;
    float _basicGolemPercentage = 1;
    int _specialTypes = 3;
    float _waveTimer = 0;
    float _waveDelay = 10;

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

    public void AddKill()
    {
        _killCount += 1;
    }
    private void Update()
    {
        CheckAmountEnemies();
        UpdateAmount();

    }
    void UpdateAmount()
    { 
        if(_spawnManager.IsDoneSpawning)
        {
            _waveTimer += Time.deltaTime;
            if(_waveTimer >= _waveDelay)
            {
                _waveTimer = 0;
                _amountToSpawn += _incrementAmount;

                if(_amountToSpawn >= 20)
                {
                    _basicGolemPercentage = 1;
                }
                else
                {
                    _basicGolemPercentage = .85f;
                }

                MakeQueue();
                _spawnManager.SetSpawnList = _spawnList;
            }
        }
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
