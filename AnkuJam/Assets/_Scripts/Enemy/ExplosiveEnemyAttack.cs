using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosiveEnemyAttack : EnemyAttack
{
    public int Damage;
    public bool IsAttacked;
    public float ExplosiveRange;
    public float ExplosiveDelay;
    public GameObject DangerZone;
    


    public override void Attack()
    {
        if (!IsAttacked) 
        {
            GetComponent<EnemyMovement>().ShouldMoveToPlayer = false;
            SoundManager.Instance.PlayOneShot(SoundManager.Sounds.explosiveBip);
            IsAttacked = true;
            DangerZone.SetActive(true);
            Invoke("Explode",ExplosiveDelay);
        }
    }

    private void Explode() 
    {
        ParticleManager.Instance.SpawnParticleObjectAtLocation(ParticleManager.Instance.ExplosionParticle, transform.position);
        Collider2D hit = Physics2D.OverlapCircle(transform.position, ExplosiveRange, AttackLayerMask);
        SoundManager.Instance.PlayOneShot(SoundManager.Sounds.explosionJellyfish);
        if (hit && hit.transform.TryGetComponent<IDamageable>(out IDamageable hitObject))
        {
            hitObject.GetDamage(Damage);
        }
        EnemyChar.Die();
    }
    private void OnDrawGizmos()
    {
        Gizmos.DrawSphere(transform.position,ExplosiveRange);
    }
}
