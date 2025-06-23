using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;
using UnityEngine.VFX;

public class SpecialSpell : MonoBehaviour
{
    [SerializeField]
    float _delay = 5;

    private VisualEffect _effect;
    public bool SetReturn { set { _returned = value; } }
    public ObjectPool<SpecialSpell> Pool { set { _pool = value; } }
    private ObjectPool<SpecialSpell> _pool;

    bool _returned = false;
    float _timer;

     void Start()
     { 
        _effect = GetComponent<VisualEffect>();
        
     }


    void Update()
    {
        if(!_returned)
        {
            _timer += Time.deltaTime;

            if(_timer >= _delay)
            {
                _timer = 0;
                _pool.Release(this);
            }
        }
    }


   



}
