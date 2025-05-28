using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

public class Player : MonoBehaviour
{

    public UnityEvent OnPlayerDeath;
    public UnityEvent SpecialAttack;

    private ForbiddenSpell _specialAttack;
    private HealthComponent _health;

    bool _isDead = false;

    // Start is called before the first frame update
    void Start()
    {
        _specialAttack = GetComponent<ForbiddenSpell>();
        _health = GetComponent<HealthComponent>();
    }

    // Update is called once per frame
    void Update()
    {
       if(_specialAttack._specialActive)
        {
            SpecialAttack.Invoke();
        }


        if(_health.Health == 0 && !_isDead)
        {
            _isDead = true;
            OnPlayerDeath.Invoke();
        }
        
    }
}
