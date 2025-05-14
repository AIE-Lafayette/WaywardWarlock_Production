using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Pool;

public class Bullet : MonoBehaviour
{

    [SerializeField]
    public float _speed;
    [SerializeField]
    public float _damage;

    public Vector3 SetDirection { set { _direction = value; } }
    public ObjectPool<Bullet> Pool { set { _pool = value; } }
    private ObjectPool<Bullet> _pool;
    private Vector3 _direction;

    void Start()
    {
           
    }

    // Update is called once per frame
    void Update()
    {

        transform.Translate(_direction * _speed * Time.deltaTime, Space.Self);


    }

    private void OnCollisionEnter(Collision collision)
    {
        
       
        if(collision.gameObject.tag == "Enemy" )
        {
            Debug.Log("Enemy Collision");
            HealthComponent health = collision.gameObject.GetComponent<HealthComponent>();
            health.RemoveHealth(_damage);
            
        _pool.Release(this);
       
        }
    }
}
