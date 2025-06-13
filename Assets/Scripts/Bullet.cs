using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Pool;
using UnityEngine.VFX;

public class Bullet : MonoBehaviour
{

    [SerializeField]
    private float _speed;
    [SerializeField]
    private float _damage;
    [SerializeField]
    private float _iceBulletDecay;

    private TrailRenderer _trail;

    public Vector3 SetDirection { set { _direction = value; } }
    public ShotType ShotType { get { return _bulletType; } set { _bulletType = value; } }
    public VisualEffect Effect { get { return _effect; } }
    private ShotType _bulletType;
    private VisualEffect _effect;
    public ObjectPool<Bullet> Pool { set { _pool = value; } }
    private ObjectPool<Bullet> _pool;
    private Vector3 _direction;
    int _boundLimit = 30;
    float _iceTimer;


    void Start()
    {
        _effect = GetComponentInChildren<VisualEffect>();
        _trail = GetComponentInChildren<TrailRenderer>();
    }
    public void ResetIceTimer()
    {
        _iceTimer = 0;
    }
    // Update is called once per frame
    void Update()
    {
        if (_bulletType == ShotType.ICE)
        {
            _iceTimer += Time.deltaTime;
            if (_iceTimer > _iceBulletDecay)
            {
                _iceTimer = 0;
                _pool.Release(this);
            }
        }
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
        HealthComponent health = collision.gameObject.GetComponent<HealthComponent>();
        if (collision.gameObject.CompareTag("Enemy") && health && health.Health != 0)
        {
            if(_bulletType != ShotType.ICE)
            {
                DamageFlash damageEffect = collision.gameObject.GetComponent<DamageFlash>();
                if (damageEffect != null)
                    damageEffect.PlayerDamageEffect();
                health.RemoveHealth(_damage);
                _pool.Release(this);
            }
            else
            {
                DamageFlash damageEffect = collision.gameObject.GetComponent<DamageFlash>();
                if (damageEffect != null)
                    damageEffect.PlayerDamageEffect();
                health.RemoveHealth(_damage);
               
            }
            
        }

        if(collision.gameObject.tag == "Environment")
        {
            _pool.Release(this);
        }
    }
}
