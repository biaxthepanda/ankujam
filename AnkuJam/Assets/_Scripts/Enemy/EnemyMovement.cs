using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    public Transform Target;

    public float StartFollowingRange;
    public Rigidbody2D RB;
    public float Speed;
    public bool ShouldMoveToPlayer = true;

    public EnemyAttack AttackEnemy;

    void Start()
    {
        Target = LevelManager.Player.transform;
    }

    
    void Update()
    {
        if(Target == null)
            Target = LevelManager.Player.transform;
        float DistanceWithTarget = Vector2.Distance(Target.position, transform.position); 

        if (DistanceWithTarget < StartFollowingRange &&
            DistanceWithTarget > AttackEnemy.AttackRange &&
            ShouldMoveToPlayer)  
        {
            MoveToPlayer(Target);
        }
        else {
            if(DistanceWithTarget < AttackEnemy.AttackRange) 
            {
                AttackEnemy.Attack();
            
            }


            if (RB.velocity.magnitude != 0) 
            {
               RB.velocity = Vector2.Lerp(RB.velocity,Vector2.zero,0.1f);
            }
            
        }
    }

    public virtual void MoveToPlayer(Transform target) 
    {
        RB.velocity = (target.position - transform.position).normalized * Speed;
    }

}
