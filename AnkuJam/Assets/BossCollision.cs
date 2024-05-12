using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static BossAttack;

public class BossCollision : MonoBehaviour
{
    public BossAttack BossAttack;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("BOSS HIT SOMETHING");
        if (collision.gameObject.CompareTag("Player") && BossAttack.State == BossStates.Melee)
        {
            Debug.Log("BOSS HIT PLAYER");
            collision.GetComponent<IDamageable>().GetDamage(BossAttack.MeleeDamage);
        }
    }
}
