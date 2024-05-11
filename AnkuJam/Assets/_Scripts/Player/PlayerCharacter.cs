using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Character
{


    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public override void GetDamage(int damage)
    {
        Debug.Log("Player Got Damage");
        base.GetDamage(damage);
    }
}
