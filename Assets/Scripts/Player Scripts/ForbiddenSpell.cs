using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;
using UnityEngine.VFX;

public class ForbiddenSpell : MonoBehaviour
{
    public void SpecialAttack()
    {
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
