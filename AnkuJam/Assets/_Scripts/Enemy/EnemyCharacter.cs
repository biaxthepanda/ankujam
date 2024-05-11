using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCharacter : Character,IDamageable
{

    public event Action OnEnemyDied;

    public GameObject OwnPrefab;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public override void GetDamage(int damage)
    {
        base.GetDamage(damage);
        Health -= damage;
        if(Health <= 0) { Die(); }
    }

    public override void Die()
    {
        base.Die();
        OnEnemyDied?.Invoke();
        Destroy(gameObject);
    }
}
