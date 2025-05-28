using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class ForbiddenSpell : MonoBehaviour
{
    [SerializeField]
    GameManager _manager;

    [SerializeField]
    EnemyPooler _pool;
  
    public bool _specialActive = false;
    
    

    public void SpecialAttack()
    {
       if(_manager.KillCount >= _manager.SpecialKillAmount)
        {
            _specialActive = true;
            Debug.Log("Doing Special Attack");
            
        }
        
    }

    



}
