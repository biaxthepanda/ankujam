using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
    public float AttackRange;
    public Transform Target;
    public LayerMask AttackLayerMask;

    public float AttackCoolDown;
    public float AttackTimer;

    public EnemyCharacter EnemyChar;

    void Start()
    {
        Target = LevelManager.Player.transform;
    }

    // Update is called once per frame
    void Update()
    {
        if(Target == null)
            Target = LevelManager.Player.transform;

        AttackTimer -= Time.deltaTime;
    }

    public virtual void Attack() 
    {
        
    }
}
