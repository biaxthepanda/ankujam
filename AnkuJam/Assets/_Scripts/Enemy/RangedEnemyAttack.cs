using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangedEnemyAttack : EnemyAttack
{
    // Start is called before the first frame update
    public Rigidbody2D Projectile;
    public float ProjectileSpeed;

    public override void Attack()
    {
        if (AttackTimer < 0)
        {
            Rigidbody2D rb = Instantiate(Projectile, transform.position, Quaternion.identity);
            rb.AddForce((Target.position - transform.position) * ProjectileSpeed);

            AttackTimer = AttackCoolDown;
        }

    }
}
