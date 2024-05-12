using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class BossAttack : EnemyAttack
{
    public Light2D BossLight;
    public Rigidbody2D RB;
    //public Transform Target;

    public int MeleeDamage;
    public int DefaultMeleeDamage;
    public int HighMeleeDamage;

    public BossStates State;

    private float _xScale;

    //MELEE DASH
    public float SecondsToWaitBeforeJump;
    public float JumpForce;
    public float SecondsToReArrange;
    public List<Transform> ReArrangePlaces;
    public int MaxJumpAmount;
    private int _currentJumpAmount;

    //RANGED
    public Transform LightTransform;
    public Rigidbody2D SmallProjectile;
    public Rigidbody2D BigProjectile;
    public float SecondsToWaitBeforeSmall;
    public float SecondsToWaitBeforeBig;
    public float ForceSmallProjectile;
    public float ForceBigProjectile;
    public int MaxRangedAmount;
    private int _currentRangedAmount;

    public float StunnedSeconds;



    // Start is called before the first frame update
    void Start()
    {
        _xScale = EnemyChar.SR.transform.localScale.x;
        ReArrangePlaces = new List<Transform>();
        foreach(var obj in GameObject.FindGameObjectsWithTag("Rearrange")) 
        {
            ReArrangePlaces.Add(obj.transform);
        }
        Target = LevelManager.Player.transform;
        MeleeDamage = DefaultMeleeDamage;
        State = BossStates.Melee;
        StartCoroutine(MeleeAttack());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public enum BossStates 
    {
        Roaming,
        Death,
        Melee,
        Ranged
    }

    IEnumerator MeleeAttack() 
    {
        yield return new WaitForSeconds(2f);
        LevelManager.Instance.ToggleGlobalLight(false,2f);
        LevelManager.Player.GetComponent<PlayerCharacter>().TogglePlayerLight(true);
        StartCoroutine(JumpToPlayer());
    }

    void SwitchBossState (BossStates state)
    {
        State = state;
        switch (State) 
        {
            case BossStates.Melee:
                _currentJumpAmount = 0;
                StartCoroutine(MeleeAttack());
                break;
            case BossStates.Ranged:
                _currentRangedAmount = 0;
                RandomRangeAttack();
                break;
            case BossStates.Roaming:
                StartCoroutine(WaitStunned());
                break;


        }
    }

    IEnumerator WaitStunned() 
    {
        yield return new WaitForSeconds(StunnedSeconds);
        SwitchBossState(BossStates.Ranged);
    }

    //Melee
    IEnumerator JumpToPlayer() 
    {
        
        BossLight.intensity = 1;
        yield return new WaitForSeconds(SecondsToWaitBeforeJump);

        FacePlayer();
        EnemyChar.CharacterAnimator.SetTrigger("Dash");
        MeleeDamage = HighMeleeDamage;
        RB.AddForce((Target.position-transform.position).normalized*JumpForce);
        yield return new WaitForSeconds(3);

        EnemyChar.CharacterAnimator.SetTrigger("StopDash");
        _currentJumpAmount++;
        if(_currentJumpAmount >= MaxJumpAmount) 
        {
            SwitchBossState(BossStates.Roaming);
            LevelManager.Instance.ToggleGlobalLight(true, 2f);
            BossLight.intensity = 0;
            LevelManager.Player.GetComponent<PlayerCharacter>().TogglePlayerLight(false);
        }
        else 
        {
           StartCoroutine(CloseLightAndMoveToAnotherPosition());
        }
        
    }
    IEnumerator CloseLightAndMoveToAnotherPosition() 
    {
        Debug.Log("ClosedLight");
        BossLight.intensity = 0;
        RB.velocity = Vector2.zero;
        MeleeDamage = DefaultMeleeDamage;
        transform.position = ReArrangePlaces[Random.Range(0,ReArrangePlaces.Count-1)].position;
        yield return new WaitForSeconds(SecondsToReArrange);
        StartCoroutine(JumpToPlayer());
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        
    }


    //Ranged
    void RandomRangeAttack() 
    {
        int rand = Random.Range(0, 2);
        EnemyChar.CharacterAnimator.SetTrigger("LightAttack");
        
        if (rand == 0) 
        {
            StartCoroutine(ManyRangeAttacks(Target));
            
        }
        else 
        {
            StartCoroutine(BigRangeAttack(Target));
        }
       
    }

    IEnumerator ManyRangeAttacks(Transform target) 
    {
        yield return new WaitForSeconds(SecondsToWaitBeforeSmall);
        FacePlayer();
        Instantiate(SmallProjectile, LightTransform.position, Quaternion.identity).AddForce((target.position-transform.position).normalized * ForceSmallProjectile);
        Instantiate(SmallProjectile, LightTransform.position, Quaternion.identity).AddForce((target.position+ new Vector3(0,2f,0)-transform.position).normalized * ForceSmallProjectile);
        Instantiate(SmallProjectile, LightTransform.position, Quaternion.identity).AddForce((target.position + new Vector3(0, -2f, 0) - transform.position).normalized * ForceSmallProjectile);
        Instantiate(SmallProjectile, LightTransform.position, Quaternion.identity).AddForce((target.position + new Vector3(0, 4f, 0) - transform.position).normalized * ForceSmallProjectile);
        Instantiate(SmallProjectile, LightTransform.position, Quaternion.identity).AddForce((target.position + new Vector3(0, -4f, 0) - transform.position).normalized * ForceSmallProjectile);
        yield return new WaitForSeconds(2);
        DecideRangedStage();
    }

    IEnumerator BigRangeAttack(Transform target)
    {
        yield return new WaitForSeconds(SecondsToWaitBeforeBig);
        FacePlayer();
        Instantiate(BigProjectile, LightTransform.position, Quaternion.identity).AddForce((target.position - transform.position).normalized * ForceSmallProjectile);
        yield return new WaitForSeconds(0.3f);
        DecideRangedStage();
    }

    void DecideRangedStage()
    {
        _currentRangedAmount++;
        if (_currentRangedAmount >= MaxRangedAmount)
        {
            SwitchBossState(BossStates.Melee);
        }
        else
        {
            RandomRangeAttack();
        }
    }

    void FacePlayer() 
    {
        int direction = Target.transform.position.x > transform.position.x ? -1 : 1;
        EnemyChar.SR.transform.localScale = new Vector3(_xScale * direction, EnemyChar.SR.transform.localScale.y, EnemyChar.SR.transform.localScale.z);
    }

    
}
