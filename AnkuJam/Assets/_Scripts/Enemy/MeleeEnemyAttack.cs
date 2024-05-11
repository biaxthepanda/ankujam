using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeEnemyAttack : EnemyAttack
{

    public int  Damage;


    public override void Attack()
    {
        if(AttackTimer < 0) 
        {
            Debug.Log("Enemy Melee Attacked");
            RaycastHit2D hit = Physics2D.Raycast(transform.position, (Target.position - transform.position).normalized, AttackRange, AttackLayerMask);


            if (hit.transform.TryGetComponent<IDamageable>(out IDamageable hitObject))
            {
                Debug.Log("Damaged");
                hitObject.GetDamage(Damage);

            }
            AttackTimer = AttackCoolDown;
        }
        
    }
}
