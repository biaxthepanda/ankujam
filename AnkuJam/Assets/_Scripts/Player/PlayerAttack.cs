using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    //MELEE
    [SerializeField] private float _attackCoolDown;
    private float _attackTimer;
    [SerializeField] private float _attackRange;
    [SerializeField] private float _damage;

    //RANGED
    [SerializeField] private Rigidbody2D _projectile;
    [SerializeField] private float _projectileSpeed;
    [SerializeField] private float _rangedAttackCoolDown;
    private float _rangedAttackTimer;


    [SerializeField] private PlayerMovement _movement;


    [SerializeField] private LayerMask _attackLayer;


    private Vector2 _attackDirection { get { return new Vector2(_movement.LastHorizontalMovement, 0) ; } set { } }

    // Start is called before the first frame update
    void Start()
    {
        _movement = GetComponent<PlayerMovement>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0)) 
        {
            if (_attackTimer < 0) 
            {
                PrimaryAttack();
                _attackTimer = _attackCoolDown;
            }
        }
        if (Input.GetMouseButtonDown(1))
        {
            if (_rangedAttackTimer < 0)
            {
                RangedAttack(Camera.main.ScreenToWorldPoint(Input.mousePosition));
                _rangedAttackTimer = _rangedAttackCoolDown;
            }
        }

        _attackTimer -= Time.deltaTime;
        _rangedAttackTimer -= Time.deltaTime;
    }

    void PrimaryAttack() 
    {
        Debug.Log("auuuu");
        //RaycastHit2D hit = Physics2D.Raycast(transform.position, _attackDirection,_attackRange,_attackLayer);
        Collider2D[] hits = Physics2D.OverlapBoxAll( transform.position+ new Vector3(_attackDirection.x,0,0), new Vector2(1,2),0f,_attackLayer);
        
        //Debug.DrawLine(transform.position,transform.position+new Vector3(_attackDirection.x,_attackDirection.y,0)*_attackRange);
        //if (hit) 
        //{
        //    Debug.Log("fýenoenp");
        //    if (hit.transform.TryGetComponent<IDamageable>(out IDamageable hitObject)) 
        //    {
        //        hitObject.GetDamage(_damage);
        //    }
        
        //}
        if(hits.Length != 0) 
        {
            Debug.Log("Player hit something");
            foreach(var hit in hits) 
            {
                if (hit.transform.TryGetComponent<IDamageable>(out IDamageable hitObject))
                {
                    hitObject.GetDamage(_damage);
                }
            }
        }
    
    }

    void RangedAttack(Vector3 attackLocation) 
    {
        Rigidbody2D rb = Instantiate(_projectile,transform.position,Quaternion.identity);
        rb.AddForce((attackLocation-transform.position)*_projectileSpeed);
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawCube(transform.position + new Vector3(_attackDirection.x, 0, 0), new Vector2(1, 2));
    }
}
