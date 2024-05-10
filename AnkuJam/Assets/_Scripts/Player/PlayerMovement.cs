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
    public float LastHorizontalMovement;



    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        _movement = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
        if (_movement.magnitude != 0)
            LastMovement = _movement.normalized;
        if (_movement.x != 0)
            LastHorizontalMovement = _movement.normalized.x;
        Debug.DrawLine(transform.position,transform.position+new Vector3(LastMovement.x,LastMovement.y,0));
    }

    private void FixedUpdate()
    {
        _rb.velocity = _movement * Speed;
    }


    private void AccelerateToSpeed(float maxSpeed, float acceleration) 
    {
            
    
    }
}
