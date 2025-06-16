using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Pool;
using UnityEngine.VFX;

public class ForbiddenSpell : MonoBehaviour
{
    public UnityEvent _forbiddenSpell;
    void SpawnSpecial(EnemyBehavior enemy)
    {
       SpecialSpell spell = EffectsPool.instance.BeamPool.Get();
        spell.gameObject.transform.position = enemy.transform.position;
    }

    public void SpecialAttack()
    {
        _forbiddenSpell.Invoke();
       if(GameManager.instance.KillCount >= GameManager.instance.SpecialKillAmount)
       {
            foreach (EnemyBehavior enemy in EnemyPooler.instance.ActiveList)
            {
                enemy.SpecialDeath();
                GameManager.instance.AddToTotalKill();
            }
            GameManager.instance.ResetKillCount();
            EnemyPooler.instance.ActiveList.Clear();
       }
        
    }

    



}
