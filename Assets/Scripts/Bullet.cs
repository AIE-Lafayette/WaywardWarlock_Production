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
    int _boundLimit = 30;

    void Start()
    {
          
    }

    // Update is called once per frame
    void Update()
    {
        
        transform.Translate(_direction * _speed * Time.deltaTime, Space.Self);

        DestroyWhenOffScreen();
    }

    void DestroyWhenOffScreen()
    {
        Vector2 screenPosition = Camera.main.WorldToScreenPoint(transform.position);

        if( screenPosition.x < -_boundLimit || screenPosition.x > Camera.main.pixelWidth +_boundLimit || screenPosition.y < -_boundLimit || screenPosition.y > Camera.main.pixelHeight + _boundLimit)
        {
            _pool.Release(this);
        }

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
