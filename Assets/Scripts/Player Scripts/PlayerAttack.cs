using System.Collections;
using System.Collections.Generic;
using UnityEditorInternal.VR;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{

    [SerializeField]
    float _delay = .5f;
    [SerializeField]
    float _iceDelay = 1;
    [SerializeField]
    float _fireDelay = 1;
    [SerializeField]
    float _lightningDelay = 1;
    [SerializeField]
    private Bullet _bulletPrefab;
    [SerializeField]
    private GameObject _bulletOffset;
    [SerializeField]
    private GameObject _meshObject;


    public ShotType SetShotType 
    { set 
        { 
            _shotType = value;
            _specialActive = true;
        } 
    }
    public float SetSpecialTimer { set { _specialTimeLeft = value; } }
    
    float _specialTimeLeft;
    float _shootingtimer;
    private ShotType _shotType;
    private bool _specialActive = false;
    void Start()
    {
        

        if(!_bulletPrefab)
        {
            Debug.LogError("PlayerAttack: No bullet prefab set!");
            return;
        }


    }

    // Update is called once per frame
    void Update()
    {
       if(!_specialActive)
       {
            _shootingtimer += Time.deltaTime;


            if(_shootingtimer > _delay)
            {
                _shootingtimer -= _delay;

                Bullet bullet = Instantiate(_bulletPrefab,_bulletOffset.transform.position, Quaternion.identity);
                Vector3 Direction = (_bulletOffset.transform.position - transform.position).normalized;
                Direction.y = 0;
                bullet.SetDirection = Direction;

            }
       }
        else
        {

            ShootSpecial();

        }
    }
    
    private void OnCollisionStay(Collision collision)
    {
       //collects 
    }

    void ShootSpecial()
    {
        switch (_shotType)
        {
            case ShotType.ICE:
                {
                    _specialTimeLeft -= Time.deltaTime;
                    _shootingtimer += Time.deltaTime;
                    if (_specialTimeLeft <= 0)
                        _specialActive = false;

                    if(_shootingtimer > _iceDelay)
                    {
                        _shootingtimer -= _iceDelay;
                       
                    }

                    break;
                }
            case ShotType.FIRE:
            {
                    break;
            }
            case ShotType.LIGHTNING:
            {
                    break;
            }
        }

    }

    
}



public enum ShotType
{
    ICE = 0,
    FIRE,
    LIGHTNING
}