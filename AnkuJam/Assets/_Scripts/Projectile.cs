using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
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
        if(collision.gameObject.layer != LayerMask.NameToLayer(_ownerLayerMask)) 
        {
            Destroy(gameObject);
            Debug.LogError(collision.name +  " " + LayerMask.LayerToName(collision.gameObject.layer) + " " + LayerMask.NameToLayer(_ownerLayerMask));
        }

    }
}
