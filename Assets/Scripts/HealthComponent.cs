using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthComponent : MonoBehaviour, IHealth
{
    [SerializeField]
    private float _health;

    [SerializeField]
    private float _maxHealth;

    public float Health { get { return _health; } }

    public void AddHealth(float amount)
    { 
        _health += amount;
    }

    public void RemoveHealth(float amount)
    {
        _health -= amount;
    }

    public void ResetHealth()
    {
        _health = _maxHealth;
    }

}
