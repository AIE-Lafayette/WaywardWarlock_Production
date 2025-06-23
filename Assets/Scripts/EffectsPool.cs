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
    private SpecialSpell _forbiddenEffect;
    public ObjectPool<SpecialSpell> BeamPool {get { return _effectsPool; }}
    public static EffectsPool instance { get; private set; }
   
    private ObjectPool<SpecialSpell> _effectsPool;
    private int _maxPoolSize = 60;
    private int _defaultPoolSize = 20;


    private void Awake()
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

    void StartPool(ref ObjectPool<SpecialSpell> pool, Func<SpecialSpell> createFunction, int initsize, int maxsize)
    {
        pool = new ObjectPool<SpecialSpell>(CreateEffect, OnTakeFromPool, OnReturnedToPool, OnDestroyEffect, false, initsize, maxsize);
    }

    SpecialSpell CreateEffect()
    {
       SpecialSpell spell = Instantiate(_forbiddenEffect, transform.position, Quaternion.identity);
        spell.Pool = _effectsPool;
        return spell;
    }

    void OnTakeFromPool(SpecialSpell spell)
    {
        spell.gameObject.SetActive(true);
        spell.SetReturn = false;
    }

    void OnReturnedToPool(SpecialSpell spell)
    {
        spell.gameObject.SetActive(false);
        spell.SetReturn = true;
    }

     void OnDestroyEffect(SpecialSpell spell)
     {
        Destroy(spell.gameObject);
     }

}
