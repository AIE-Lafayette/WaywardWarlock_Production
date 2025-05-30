using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;
using UnityEngine.VFX;

public class SpecialSpell : MonoBehaviour
{
    [SerializeField]
    private float _time;


    public ObjectPool<SpecialSpell> Pool { set { _pool = value; } }
    private ObjectPool<SpecialSpell> _pool;

    private void Update()
    {
        ReleaseOverTime();
    }


    void ReleaseOverTime()
    {
        if(_time - Time.deltaTime <= 0)
        {
            _pool.Release(this);
        }
    }





}
