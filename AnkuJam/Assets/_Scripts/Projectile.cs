using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour ,IDamageable
{
    [SerializeField] private int _damage;
    [SerializeField] private string _attackLayerMask;
    [SerializeField] private string _ownerLayerMask;

    private void Start()
    {

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.layer == LayerMask.NameToLayer(_attackLayerMask)) 
        {
            if (collision.TryGetComponent<IDamageable>(out IDamageable hitCharacter)) 
            {
                hitCharacter.GetDamage(_damage);
            }
            
        }
        

    }

    public void GetDamage(int damage)
    {
        //Destroy(gameObject);
    }
}
