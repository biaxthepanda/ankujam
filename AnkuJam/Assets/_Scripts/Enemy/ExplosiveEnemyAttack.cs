using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosiveEnemyAttack : EnemyAttack
{
    public int Damage;
    public bool IsAttacked;
    public float ExplosiveRange;
    public float ExplosiveDelay;
    


    public override void Attack()
    {
        if (!IsAttacked) 
        {
            GetComponent<EnemyMovement>().ShouldMoveToPlayer = false;
            IsAttacked = true;
            Invoke("Explode",ExplosiveDelay);
        }
    }

    private void Explode() 
    {
        
        Collider2D hit = Physics2D.OverlapCircle(transform.position,ExplosiveRange,AttackLayerMask);
        if(hit.transform.TryGetComponent<IDamageable>(out IDamageable hitObject)) 
        {
            hitObject.GetDamage(Damage);
        }
    }
    private void OnDrawGizmos()
    {
        Gizmos.DrawSphere(transform.position,ExplosiveRange);
    }
}
