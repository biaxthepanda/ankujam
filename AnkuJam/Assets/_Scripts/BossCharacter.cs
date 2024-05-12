using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class BossCharacter : EnemyCharacter,IDamageable
{

    

    // Start is called before the first frame update
    void Start()
    {
        UIManager.Instance.ToggleBossHealth(true);
    }

    private void OnDestroy()
    {
        UIManager.Instance.ToggleBossHealth(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public override void GetDamage(int damage)
    {
        base.GetDamage(damage);
        UIManager.Instance.UpdateBossHealthBar(Health/MaxHealth);
    }
}
