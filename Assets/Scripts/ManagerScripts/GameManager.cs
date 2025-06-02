using System.Collections;
using System.Collections.Generic;
using System.Linq;
<<<<<<< HEAD
using UnityEditor.ShaderKeywordFilter;
using UnityEngine;
=======
using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.SceneManagement;
>>>>>>> parent of 055ffb5 (Revert "Merge branch 'Dev' into Jack")

public class GameManager : MonoBehaviour
{
    [SerializeField]
<<<<<<< HEAD
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
=======
    private int _specialKillAmount = 150;
    [SerializeField]
    GameObject _playerObject;
    [SerializeField]
    private SpawnPointManager _spawnManager;
    [SerializeField]
    private int _maxEnemiesOnScreen = 50;
    [SerializeField]
    private float _waveDelay = 10;
    [SerializeField]
    private int _incrementAmount = 5;
    public int SpecialKillAmount { get { return _specialKillAmount; } }
    public int KillCount 
    {
        get 
        {
            if (_killCount >= _specialKillAmount)
                return _specialKillAmount;
            else
                return _killCount;
        } 
        set 
        { 
            _killCount += Mathf.Abs(value); 
            _totalKillCount += Mathf.Abs(value); 
        } 
    }
    public int TotalKillCount { get { return _totalKillCount; } }

    public float TimeElapsed { get { return _timeElapsed; } }

    
    private Queue<EnemyType> _spawnList;
    public static GameManager instance;
    int _amountToSpawn = 10;
    int _totalKillCount;
    int _killCount;
    float _basicGolemPercentage = 1;
    int _specialTypes = 3;
    float _waveTimer = 0;
    float _timeElapsed;
    private void Awake()
>>>>>>> parent of 055ffb5 (Revert "Merge branch 'Dev' into Jack")
    {
        if (instance != null && instance != this)
        {
            Destroy(this);
        }
        else
        {
            instance = this;
        }
<<<<<<< HEAD

=======
    }
    private void Start()
    {
>>>>>>> parent of 055ffb5 (Revert "Merge branch 'Dev' into Jack")
        _spawnList = new Queue<EnemyType>();
        if (_spawnManager == null)
        {
            Debug.LogWarning("GameManager: Spawn Point Manager is null!"); 
            return;
        }
        MakeQueue();

        _spawnManager.SetSpawnList = _spawnList;
       
    }

<<<<<<< HEAD

    private void Update()
    {
        CheckAmountEnemies();

    }

    void CheckAmountEnemies()
    {
        if (EnemyPooler.instance.AllActive >= _maxEnemiesOnScreen)
        {
            if (_spawnManager.StopSpawning != true)
                _spawnManager.StopSpawning = true;
        }
        else
        {
            if (_spawnManager.StopSpawning != false)
                _spawnManager.StopSpawning = false;
=======
    public void AddToTotalKill()
    {
        _totalKillCount += 1;
    }
    public void ResetKillCount()
    {
        _killCount = 0;
    }
    public void AddKill()
    {
        _killCount += 1;
        _totalKillCount += 1;
    }
    private void Update()
    {
        _timeElapsed += Time.deltaTime;
        CheckAmountEnemies();
        UpdateAmount();
       

    }
    void UpdateAmount()
    {
        if (_spawnManager == null)
            return;

        if(_spawnManager.IsDoneSpawning)
        {
            _waveTimer += Time.deltaTime;
            if(_waveTimer >= _waveDelay)
            {
                _waveTimer -= _waveDelay;
                _amountToSpawn += _incrementAmount;

                if(_amountToSpawn <= 20)
                {
                    _basicGolemPercentage = 1.0f;
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
        if(EnemyPooler.instance != null)
        {
            if (EnemyPooler.instance.AllActiveCount >= _maxEnemiesOnScreen)
            {
                if (_spawnManager.ScreenFull != true)
                    _spawnManager.ScreenFull = true;
            }
            else
            {
                if (_spawnManager.ScreenFull != false)
                    _spawnManager.ScreenFull = false;
            }
>>>>>>> parent of 055ffb5 (Revert "Merge branch 'Dev' into Jack")
        }
    }    
    void MakeQueue()
    {
       
        int basicGolems = (int)(_amountToSpawn * _basicGolemPercentage);
        int specialGolems = _amountToSpawn - basicGolems;

<<<<<<< HEAD
        int amoutPerSpecial = specialGolems / _specialTypes;
=======
        int amountPerSpecial = specialGolems / _specialTypes;
>>>>>>> parent of 055ffb5 (Revert "Merge branch 'Dev' into Jack")
        int remainder = specialGolems % _specialTypes;

        List<EnemyType> tempList = new List<EnemyType>();

        //Add basic Enemys
<<<<<<< HEAD
        for(int i = 0; i < _amountToSpawn; i++)
=======
        for(int i = 0; i < basicGolems; i++)
>>>>>>> parent of 055ffb5 (Revert "Merge branch 'Dev' into Jack")
        {
            tempList.Add(EnemyType.BASE);
        }
        //Add Special Enemies
<<<<<<< HEAD
        for (int i = 0; i < amoutPerSpecial; i++)
=======
        for (int i = 0; i < amountPerSpecial; i++)
>>>>>>> parent of 055ffb5 (Revert "Merge branch 'Dev' into Jack")
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

<<<<<<< HEAD
        //Do this so you never get a scecial golem on start
        for (int i = 0; i < 10; i++)
        {
            tempList.Insert(0, EnemyType.BASE);
        }

=======
        _spawnList.Clear();
>>>>>>> parent of 055ffb5 (Revert "Merge branch 'Dev' into Jack")
        //Add to the Queue
        foreach (EnemyType type in tempList)
        {
            _spawnList.Enqueue(type);
        }
<<<<<<< HEAD
    }

=======


    }
>>>>>>> parent of 055ffb5 (Revert "Merge branch 'Dev' into Jack")
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


<<<<<<< HEAD
=======
    public void RestartLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
    public void NextScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
    public void ExitApplication()
    {
        Application.Quit();
    }

>>>>>>> parent of 055ffb5 (Revert "Merge branch 'Dev' into Jack")
}
