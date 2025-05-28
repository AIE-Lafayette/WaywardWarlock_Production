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
    private VisualEffect _forbiddenEffect;
    public ObjectPool<ForbiddenSpell> EffectPool {get { return _effectsPool; }}
    public static EffectsPool instance { get; private set; }
   
    private ObjectPool<ForbiddenSpell> _effectsPool;
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

    void StartPool(ref ObjectPool<ForbiddenSpell> pool, Func<ForbiddenSpell> createFunction, int initsize, int maxsize)
    {
        pool = new ObjectPool<ForbiddenSpell>(createFunction, OnTakeFromPool, OnReturnedToPool, OnDestroyEffect, false, initsize, maxsize);
    }

    ForbiddenSpell CreateEffect()
    {
       ForbiddenSpell spell =  Instantiate(_forbiddenEffect, transform.position, Quaternion.identity);
        return spell;
    }

    void OnTakeFromPool(ForbiddenSpell spell)
    {
        spell.gameObject.SetActive(true);
    }

    void OnReturnedToPool(ForbiddenSpell spell)
    {
        spell.gameObject.SetActive(false);
    }

    private void OnDestroyEffect(ForbiddenSpell spell)
    {
        Destroy(spell.gameObject);
    }






}
