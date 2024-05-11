using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCharacter : Character,IDamageable
{

    public event Action OnEnemyDied;
    public EnemyAttack EnemyAtt;

    private bool IsTargetOnRight;

    // Start is called before the first frame update
    void Start()
    {
        EnemyAtt = GetComponent<EnemyAttack>();
    }

    // Update is called once per frame
    void Update()
    {
       
        if (IsTargetOnRight) 
        {
            if((EnemyAtt.Target.position - transform.position).normalized.x < 0) 
            {
                SR.flipX = false;
                IsTargetOnRight = false;
            
            }
        }
        else 
        {
            if ((EnemyAtt.Target.position - transform.position).normalized.x > 0) 
            {
                SR.flipX = true;
                IsTargetOnRight = true;
            }
        }
        
    }

    public override void GetDamage(int damage)
    {
        base.GetDamage(damage);
        SoundManager.Instance.PlayOneShot(SoundManager.Sounds.damage);
        CharacterAnimator.SetTrigger("Hit");
        Health -= damage;
        if(Health <= 0) { Die(); }
    }

    public override void Die()
    {
        base.Die();
        OnEnemyDied?.Invoke();
        Invoke("DestroyEnemy", 1f);
    }

    public void DestroyEnemy() 
    {
        Destroy(gameObject);
    }
}
