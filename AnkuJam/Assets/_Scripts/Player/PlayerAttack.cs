using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.UI;

public class PlayerAttack : MonoBehaviour
{
    //MELEE
    [SerializeField] private float _attackCoolDown;
    private float _attackTimer;
    [SerializeField] private float _attackRange;
    [SerializeField] private int _damage;
    public GameObject SlashEffect;

    //RANGED
    [SerializeField] private Rigidbody2D _projectile;
    [SerializeField] private float _projectileSpeed;
    [SerializeField] private float _rangedAttackCoolDown;
    private float _rangedAttackTimer;
    public Image RangedCooldownImage;


    [SerializeField] private PlayerMovement _movement;


    [SerializeField] private LayerMask _attackLayer;
    [SerializeField] private float _meleeAttackForce;

    private Vector2 _attackDirection { get { return new Vector2(_movement.LastHorizontalMovement, 0) ; } set { } }

    public PlayerCharacter PlayerChar;

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

        RangedCooldownImage.fillAmount = (_rangedAttackCoolDown - Mathf.Clamp(_rangedAttackTimer,0,_rangedAttackCoolDown+0.1f)) / _rangedAttackCoolDown;

        _attackTimer -= Time.deltaTime;
        _rangedAttackTimer -= Time.deltaTime;
    }

    void PrimaryAttack() 
    {
        Debug.Log("auuuu");
        //RaycastHit2D hit = Physics2D.Raycast(transform.position, _attackDirection,_attackRange,_attackLayer);
        Collider2D[] hits = Physics2D.OverlapBoxAll( transform.position + new Vector3(_attackDirection.x*2,0,0), new Vector2(2f,4),0f,_attackLayer);
        SoundManager.Instance.PlayOneShot(SoundManager.Sounds.gillWater);
        PlayerChar.CharacterAnimator.SetTrigger("Melee");

        float slashDirection = GetComponent<PlayerMovement>().SR.flipX ? 180 : 0;
        Instantiate(SlashEffect, transform.position + new Vector3(_attackDirection.x * 2, 0, 0), Quaternion.Euler(0,0, slashDirection));
        if(hits.Length != 0) 
        {
            ParticleManager.Instance.SpawnParticleAtLocation(ParticleManager.Instance.BubbleParticle, hits[0].transform.position);
            
            Debug.Log("Player hit something");
            DOVirtual.DelayedCall(0.01f, () => 
            {
                foreach (var hit in hits)
                {
                    if (hit.transform.TryGetComponent<IDamageable>(out IDamageable hitObject))
                    {
                        Debug.Log("Damaged");
                        hit.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
                        hit.GetComponent<Rigidbody2D>().AddForce((hit.transform.position - transform.position).normalized * _meleeAttackForce);
                        hitObject.GetDamage(_damage);

                    }
                }
            }
            );
            
        }
    
    }

    void RangedAttack(Vector3 attackLocation) 
    {
        SoundManager.Instance.PlayOneShot(SoundManager.Sounds.Gunshot);

        PlayerChar.CharacterAnimator.SetTrigger("Ranged");
        Rigidbody2D rb = Instantiate(_projectile,transform.position,Quaternion.identity);
        rb.AddForce((attackLocation-transform.position)*_projectileSpeed);
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawCube(transform.position + new Vector3(_attackDirection.x*2, 0, 0), new Vector2(2f, 4));
    }
}
