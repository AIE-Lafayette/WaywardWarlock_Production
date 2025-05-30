using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;
using UnityEngine.VFX;

public class ForbiddenSpell : MonoBehaviour
{
    [SerializeField]
    GameManager _manager;

    [SerializeField]
    EnemyPooler _serializedPool;



    

    public void SpecialAttack()
    {
       if(_manager.KillCount >= _manager.SpecialKillAmount)
       {
            List<EnemyBehavior> Enemies = EnemyPooler.instance.ActiveList;
            int count = Enemies.Count;
            for (int i = 0; i < count; i++)
            {
                Enemies[i].SpecialDeath();
            }
            _manager.ResetKillCount();
       }
        
    }

    



}
