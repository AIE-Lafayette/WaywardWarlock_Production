using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemDrop : MonoBehaviour, ICollectible
{
    [SerializeField]
    ShotType _type;

    [SerializeField]
    float _delay;

    [SerializeField]
    float _time;

    [SerializeField]
    float _damage;


    public float Delay {get{return _delay;}} 

    public float Time { get { return _time; } }

    public float Damage { get { return _damage; } }

    public ShotType BulletType { get { return _type; } }
}
