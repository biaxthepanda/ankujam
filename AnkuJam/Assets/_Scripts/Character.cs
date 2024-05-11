using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour,IDamageable
{
    public float MaxHealth;
    public float Health;
    public SpriteRenderer SR;

    public Animator CharacterAnimator;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void Attack() 
    {
    }

    public virtual void GetDamage(int damage)
    {
       
    }

    public virtual void Die() { }
}
