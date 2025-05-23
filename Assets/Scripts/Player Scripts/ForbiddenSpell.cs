using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class ForbiddenSpell : MonoBehaviour
{
    EnemyPooler _pool;
    GameManager _manger;

    private void Awake()
    {
        _pool = EnemyPooler.instance;
        _manger = GameManager.instance;
    }



    private void Update()
    {
        SpecialAttack();
    }

    public void SpecialAttack()
    {
       if(_manger.KillCount >= 5)
        {
            foreach (EnemyBehavior enemy in _pool.ActiveList)
            {
                enemy.StopMovement = true;
            }
        }
        
    }



}
