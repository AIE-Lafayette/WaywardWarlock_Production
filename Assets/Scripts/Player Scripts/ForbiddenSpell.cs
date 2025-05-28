using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class ForbiddenSpell : MonoBehaviour
{
    [SerializeField]
    GameManager _manager;

    [SerializeField]
    EnemyPooler _serializedPool;



    private ObjectPool<ForbiddenSpell> _pool;
    public ObjectPool<ForbiddenSpell> Pool { set { _pool = value; } }

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
