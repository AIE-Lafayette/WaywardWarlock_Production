using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;
using UnityEngine.VFX;

public class SpecialSpell : MonoBehaviour
{
    [SerializeField]
    public float _time;


    public ObjectPool<SpecialSpell> Pool { set { _pool = value; } }
    private ObjectPool<SpecialSpell> _pool;

     void Start()
    {
        
    }


    void Update()
    {
        _time -= Time.deltaTime;

        if (_time <= 0)
            _pool.Release(this);
    }


   



}
