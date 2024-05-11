using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class BossCharacter : IDamageable
{

    public float Health;
    public void GetDamage(int damage)
    {
        Health -= damage;
        if(Health<= 0) 
        {
            Die();   
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void Die() 
    {
    
    }
}
