using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;
using UnityEngine.Pool;
using UnityEngine.VFX;
public class EffectsPool : MonoBehaviour
{
    [SerializeField]
    private GameObject _forbiddenEffect;
    public ObjectPool<GameObject> EffectPool {get { return _effectsPool; }}
    public static EffectsPool instance { get; private set; }
   
    private ObjectPool<GameObject> _effectsPool;
    private int _maxPoolSize = 60;
    private int _defaultPoolSize = 20;


    public void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(this);
        }
        else
        {
            instance = this;
        }
        StartPool(ref _effectsPool, CreateEffect, _defaultPoolSize, _maxPoolSize);
    }

    void StartPool(ref ObjectPool<GameObject> pool, Func<GameObject> createFunction, int initsize, int maxsize)
    {
        pool = new ObjectPool<GameObject>(createFunction, OnTakeFromPool, OnReturnedToPool, OnDestroyEffect, false, initsize, maxsize);
    }

    GameObject CreateEffect()
    {
       GameObject spell = Instantiate(_forbiddenEffect, transform.position, Quaternion.identity);
        return spell;
    }

    void OnTakeFromPool(GameObject spell)
    {
        spell.gameObject.SetActive(true);
    }

    void OnReturnedToPool(GameObject spell)
    {
        spell.gameObject.SetActive(false);
    }

    private void OnDestroyEffect(GameObject spell)
    {
        Destroy(spell.gameObject);
    }






}
