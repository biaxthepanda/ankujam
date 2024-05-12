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

    public float DashForce;
    public float DashCoolDown;
    private float _dashTimer;
    private bool _dashed = false;

    public PlayerCharacter PlayerChar;
    public SpriteRenderer SR;
    public Animator PlayerAnimator;
    public SpriteRenderer DashIcon;

    public AudioSource SwimmingAudSrc;


    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (PlayerChar.Health <= 0) return;

        _movement = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));


        _dashTimer -= Time.deltaTime;
        if(_dashTimer <= 0) 
        {
            UIManager.Instance.ToggleDashImage(true);
            DashIcon.gameObject.SetActive(true);
        }

        IsMovementChanged(_movement,LastMovement);
        
            LastMovement = _movement;
        if (_movement.x != 0) 
        {
            LastHorizontalMovement = _movement.normalized.x;
            SR.flipX = LastHorizontalMovement > 0 ? false : true;
        }
        if (Input.GetKeyDown(KeyCode.LeftShift)) 
        {
            Dash();
        }
            
        PlayerAnimator.SetFloat("Speed",_rb.velocity.magnitude);
        Debug.DrawLine(transform.position,transform.position+new Vector3(LastMovement.x,LastMovement.y,0));
    }

    private void FixedUpdate()
    {
        if(_dashed == false) 
        {
            _rb.velocity = _movement * Speed;
        }
    }


    private void Dash() 
    {
        if(_dashTimer <= 0) 
        {
            _dashed = true;
            UIManager.Instance.ToggleDashImage(false);
            DashIcon.gameObject.SetActive(false);
            Debug.Log("DASHED");
            _rb.AddForce(_movement.normalized * DashForce,ForceMode2D.Impulse);
            SoundManager.Instance.PlayOneShot(SoundManager.Sounds.playerDash);
            StartCoroutine(ResetDash());
            _dashTimer = DashCoolDown;
        }
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

    IEnumerator ResetDash() 
    {
        yield return new WaitForSeconds(0.4f);

        _dashed = false;
    }
}
