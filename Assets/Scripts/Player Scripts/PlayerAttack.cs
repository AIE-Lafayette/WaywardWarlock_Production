using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PlayerAttack : MonoBehaviour
{


    [SerializeField]
    float _baseDelay = .5f;

    float _delay = .5f;
   
    [SerializeField]
    private GameObject _bulletOffset;
    [SerializeField]
    private GameObject _meshObject;

    public float SpecialTimeLeft { get { return _specialTimeLeft; } }
    public bool SpecialActive { get { return _specialActive; } }
    
    public ShotType SetShotType 
    { set 
        { 
            _shotType = value;
            _specialActive = true;
        } 
    }


    bool _isDead = false;
    float _specialTimeLeft;
    float _shootingtimer;
    private ShotType _shotType;
    private bool _specialActive = false;

    private void Start()
    {
        _shotType = new ShotType();
        _shotType = ShotType.BASIC;
    }
    // Update is called once per frame
    void Update()
    {
       _shootingtimer += Time.deltaTime;
        if (_specialActive)
            _specialTimeLeft -= Time.deltaTime;
       if(_shootingtimer > _delay)
       {
           _shootingtimer -= _delay;
            if(_specialTimeLeft <= 0 && _specialActive == true)
            {
                _specialTimeLeft = 0;
                _shotType = ShotType.BASIC;
                _specialActive = false;
                _delay = _baseDelay;
            }
            Shoot();
       }
   
    }
    private void OnTriggerEnter(Collider other)
    {
        ICollectible item = other.gameObject.GetComponent<ICollectible>();
        if (item != null)
        {
            if (!_specialActive)
            {
                SetShotType = item.BulletType;
                _delay = item.Delay;
                _specialTimeLeft = item.Time;
                Destroy(other.gameObject);
            }
        }
    }
    private void OnCollisionStay(Collision collision)
    {
        //checks first if special is active or not

        ICollectible item = collision.gameObject.GetComponent<ICollectible>();
        if (item != null)
        {
            if(!_specialActive)
            {
                SetShotType = item.BulletType;
                _delay = item.Delay;
                _specialTimeLeft = item.Time;
                Destroy(collision.gameObject);
            }
        }
    }

    void ShootBullet(Bullet bullet)
    {
        if(!_isDead)
        {
            float Angle = Vector3.Angle(_meshObject.transform.position, _bulletOffset.transform.position);
            bullet.transform.position = _bulletOffset.transform.position;
            Vector3 Direction = (_bulletOffset.transform.position - transform.position).normalized;
            bullet.transform.rotation = Quaternion.Euler(new Vector3(0, Angle, 0));
            Direction.y = 0;
            bullet.SetDirection = Direction;
        }
    }
    
    void Shoot()
    {
        switch (_shotType)
        {
            case ShotType.BASIC:
                {

                    ShootBullet(BulletPool.instance.BasicBulletPool.Get());     
                     break;
                 }
            case ShotType.ICE:
                {
                    ShootBullet(BulletPool.instance.IceBulletPool.Get());
                  break;
                }
            case ShotType.FIRE:
                {
                  
                    ShootBullet(BulletPool.instance.FireBulletPool.Get());
                    break;
                }
            case ShotType.LIGHTNING:
                {
                    ShootBullet(BulletPool.instance.LightningBulletPool.Get());
                    break;
                }
          
        }

    }


    public void StopShooting()
    {
        _isDead = true;
    }
}



public enum ShotType
{
    BASIC = 0,
    ICE,
    FIRE,
    LIGHTNING
}