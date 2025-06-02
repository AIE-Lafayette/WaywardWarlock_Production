using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class EnemyPooler : MonoBehaviour
{


    [SerializeField]
    private EnemyBehavior _baseGolemPrefab;
    [SerializeField]
    private EnemyBehavior _fireGolemPrefab;
    [SerializeField]
    private EnemyBehavior _iceGolemPrefab;
    [SerializeField]
    private EnemyBehavior _lightningGolemPrefab;
    public static EnemyPooler instance { get; private set; }
<<<<<<< HEAD
    
    public int AllActive 
=======
    private List<EnemyBehavior> _activeList;

    public List<EnemyBehavior> ActiveList { get { return _activeList; } }
    public int AllActiveCount 
>>>>>>> parent of 055ffb5 (Revert "Merge branch 'Dev' into Jack")
    { get 
      { 
        return _baseGolemPool.CountActive + _iceGolemPool.CountActive + _fireGolemPool.CountActive + _lightningGolemPool.CountActive; 
      } 
    }


    int _baseGolemPoolSize = 100;
    int _baseGolemMaxPoolSize = 300;

    int _specialGolemPoolSize = 20;
    int _specialGolemMaxPoolSize = 100;


    private ObjectPool<EnemyBehavior> _baseGolemPool;
    private ObjectPool<EnemyBehavior> _iceGolemPool;
    private ObjectPool<EnemyBehavior> _fireGolemPool;
    private ObjectPool<EnemyBehavior> _lightningGolemPool;


    public ObjectPool<EnemyBehavior> BaseGolemPool { get { return _baseGolemPool; } }
    public ObjectPool<EnemyBehavior> IceGolemPool { get { return _iceGolemPool; } }
    public ObjectPool<EnemyBehavior> FireGolemPool { get { return _fireGolemPool; } }
    public ObjectPool<EnemyBehavior> LightningGolemPool { get { return _lightningGolemPool; } }



    private void Awake()
    {
        if(instance != null && instance != this)
        {
            Destroy(this);
        }
        else
        {
            instance = this;
        }
<<<<<<< HEAD
=======
        _activeList = new List<EnemyBehavior>();
>>>>>>> parent of 055ffb5 (Revert "Merge branch 'Dev' into Jack")

        if (!_baseGolemPrefab)
            Debug.LogError("EnemyPooler: Base Golem prefab missing!");

        if (!_fireGolemPrefab)
            Debug.LogError("EnemyPooler: Fire Golem prefab missing!");

        if (!_iceGolemPrefab)
            Debug.LogError("EnemyPooler: Ice Golem prefab missing!");

        if (!_lightningGolemPrefab)
            Debug.LogError("EnemyPooler: Lightning Golem prefab missing!");

        StartPool(ref _baseGolemPool, CreateBaseGolem,_baseGolemPoolSize,_baseGolemMaxPoolSize);
        StartPool(ref _iceGolemPool, CreateIceGolem,_specialGolemPoolSize,_specialGolemMaxPoolSize);
        StartPool(ref _fireGolemPool, CreateFireGolem,_specialGolemPoolSize,_specialGolemMaxPoolSize);
        StartPool(ref _lightningGolemPool, CreateLightningGolem,_specialGolemPoolSize,_specialGolemMaxPoolSize);

    }

<<<<<<< HEAD
=======
    public void StopAllActive()
    {
        foreach (EnemyBehavior enemy in _activeList)
        {
            enemy.StopMovement = true;
        }
    }
>>>>>>> parent of 055ffb5 (Revert "Merge branch 'Dev' into Jack")

    #region Pool Functions
    void StartPool(ref ObjectPool<EnemyBehavior> pool, Func<EnemyBehavior> createFunction,int initsize,int maxsize)
    {
<<<<<<< HEAD
        pool = new ObjectPool<EnemyBehavior>(createFunction, OnGetFromPool, OnRelease, OnDestroyPoolObject,false,initsize,maxsize);
=======
        pool = new ObjectPool<EnemyBehavior>(createFunction, OnGetFromPool, OnRelease, OnDestroyPoolObject,true,initsize,maxsize);
>>>>>>> parent of 055ffb5 (Revert "Merge branch 'Dev' into Jack")
    }


    EnemyBehavior CreateBaseGolem()
    {
        EnemyBehavior golem = Instantiate(_baseGolemPrefab);
        golem.Pool = _baseGolemPool;
        return golem;
    }

    EnemyBehavior CreateFireGolem()
    {
        EnemyBehavior golem = Instantiate(_fireGolemPrefab);
        golem.Pool = _fireGolemPool;
        return golem;
    }
    EnemyBehavior CreateIceGolem()
    {
       EnemyBehavior golem = Instantiate(_iceGolemPrefab);
        golem.Pool = _iceGolemPool;
        return golem;
    }
    EnemyBehavior CreateLightningGolem()
    {
       EnemyBehavior golem = Instantiate(_lightningGolemPrefab);
        golem.Pool = _lightningGolemPool;
        return golem;
    }

    private void OnGetFromPool(EnemyBehavior pooledObject)
    {
        pooledObject.gameObject.SetActive(true);
<<<<<<< HEAD
=======
        pooledObject.IsKilled = false;
        
>>>>>>> parent of 055ffb5 (Revert "Merge branch 'Dev' into Jack")
    }

    private void OnRelease(EnemyBehavior pooledObject)
    {
        pooledObject.gameObject.SetActive(false);
    }

    private void OnDestroyPoolObject(EnemyBehavior pooledObject)
    {
        Destroy(pooledObject.gameObject);
    }
    #endregion 


}
