using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class BulletPool : MonoBehaviour
{
    public Bullet bullet;
    public IObjectPool<Bullet> pool;

    
    private void Awake()
    {
        bullet = GetComponent<Bullet>();
       


    }
    



    
    void Update()
    {
        
    }
}
