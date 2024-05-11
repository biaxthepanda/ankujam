using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField]
    private Rigidbody2D _rb;

    public float Speed;
    private Vector2 _movement;
    public Vector2 LastMovement;
    public float LastHorizontalMovement = 1;

    public SpriteRenderer SR;
    public Animator PlayerAnimator;


    public AudioSource SwimmingAudSrc;


    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        _movement = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
        IsMovementChanged(_movement,LastMovement);
        //if (_movement.magnitude != 0)
            LastMovement = _movement;
        if (_movement.x != 0) 
        {
            LastHorizontalMovement = _movement.normalized.x;
            SR.flipX = LastHorizontalMovement > 0 ? false : true;
        }
            
        PlayerAnimator.SetFloat("Speed",_rb.velocity.magnitude);
        Debug.DrawLine(transform.position,transform.position+new Vector3(LastMovement.x,LastMovement.y,0));
    }

    private void FixedUpdate()
    {
        _rb.velocity = _movement * Speed;
    }


    private void AccelerateToSpeed(float maxSpeed, float acceleration) 
    {
            
    
    }

    private void IsMovementChanged(Vector2 movement,Vector2 lastMovement) 
    {
        if(movement.magnitude == 0 && lastMovement.magnitude != 0) 
        {
            SwimmingAudSrc.Stop();        
        }
        else if(movement.magnitude != 0 && lastMovement.magnitude == 0) 
        {
            SwimmingAudSrc.Play();
        }
    }
}
