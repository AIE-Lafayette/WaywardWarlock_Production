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
<<<<<<< HEAD

=======
    public float MaxHealth { get { return _maxHealth; } }
>>>>>>> parent of 055ffb5 (Revert "Merge branch 'Dev' into Jack")
    public void AddHealth(float amount)
    {
        _health = (_health + amount > _maxHealth) ? _health = _maxHealth : _health += amount;
    }

    public void RemoveHealth(float amount)
    {
        _health = (_health - amount < 0) ? _health = 0 : _health -= amount;
<<<<<<< HEAD
=======
        
>>>>>>> parent of 055ffb5 (Revert "Merge branch 'Dev' into Jack")
    }

    public void ResetHealth()
    {
        _health = _maxHealth;
    }

}
