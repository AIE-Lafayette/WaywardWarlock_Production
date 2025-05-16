using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Player : MonoBehaviour
{

    public UnityEvent OnPlayerDeath;

    private HealthComponent _health;
    // Start is called before the first frame update
    void Start()
    {
        _health = GetComponent<HealthComponent>();
    }

    // Update is called once per frame
    void Update()
    {

        if(_health.Health == 0)
        {
            OnPlayerDeath.Invoke();
        }
        
    }
}
