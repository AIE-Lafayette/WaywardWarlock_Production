using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class BulletPool : MonoBehaviour
{
    [SerializeField]
    private Bullet _basicBullet;
    [SerializeField]
    private Bullet _fireBullet;
    [SerializeField]
    private Bullet _lightningBullet;
    [SerializeField]
    private Bullet _iceBullet;

    private ObjectPool<Bullet> _basicPool;
    private ObjectPool<Bullet> _firePool;
    private ObjectPool<Bullet> _lightningPool;
    private ObjectPool<Bullet> _icePool;
    
    public ObjectPool<Bullet> BasicBullet { get { return _basicPool; } }
    public ObjectPool<Bullet> FireBullet { get { return _firePool; } }
    public ObjectPool<Bullet> LightningBullet { get { return _lightningPool; } }
    public ObjectPool<Bullet> IceBullet { get { return _icePool; } }


    private int _maxPoolSize = 100;
    private int _defaultCapacity = 50;

    private int _maxSpecialSize = 100;

    IObjectPool<Bullet> m_Pool;
    
    

    private void Awake()
    {

        StartPool(ref _basicPool, CreateBasicBullet, _defaultCapacity, _maxPoolSize);

    }
    
    void StartPool(ref ObjectPool<Bullet> pool, Func<Bullet> createFunction, int initsize, int maxsize)
    {
        pool = new ObjectPool<Bullet>(createFunction, OnTakeFromPool, OnReturnedToPool, OnDestroyObject, false, initsize, maxsize);
    }    

    Bullet CreateBasicBullet()
    {
        Bullet bullet = Instantiate(_basicBullet);
        
    }



   

    void OnTakeFromPool(Bullet bullet)
    {
        bullet.gameObject.SetActive(true);
    }


    void OnReturnedToPool(Bullet bullet)
    {
        bullet.gameObject.SetActive(false);
    }

    private void OnDestroyObject(Bullet bullet)
    {
        Destroy(bullet.gameObject);
    }


    void Update()
    {
        
    }
}
