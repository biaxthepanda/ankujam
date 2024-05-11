using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class BossAttack : MonoBehaviour
{
    public Light2D BossLight;
    public Rigidbody2D RB;
    public Transform Target;

    private int _meleeDamage;
    public int DefaultMeleeDamage;
    public int HighMeleeDamage;

    public BossStates State;

    //MELEE DASH
    public float SecondsToWaitBeforeJump;
    public float JumpForce;
    public float SecondsToReArrange;
    public Transform[] ReArrangePlaces;
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


    // Start is called before the first frame update
    void Start()
    {
        
        Target = LevelManager.Player.transform;
        _meleeDamage = DefaultMeleeDamage;
        State = BossStates.Ranged;
        MeleeAttack();
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

    void MeleeAttack() 
    {
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
                MeleeAttack();
                break;
            case BossStates.Ranged:
                _currentRangedAmount = 0;
                RandomRangeAttack();
                break;

        }
    }

    //Melee
    IEnumerator JumpToPlayer() 
    {
        BossLight.intensity = 1;
        yield return new WaitForSeconds(SecondsToWaitBeforeJump);
        _meleeDamage = HighMeleeDamage;
        RB.AddForce((Target.position-transform.position).normalized*JumpForce);
        yield return new WaitForSeconds(3);
        _currentJumpAmount++;
        if(_currentJumpAmount >= MaxJumpAmount) 
        {
            SwitchBossState(BossStates.Ranged);
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
        _meleeDamage = DefaultMeleeDamage;
        transform.position = ReArrangePlaces[Random.Range(0,ReArrangePlaces.Length-1)].position;
        yield return new WaitForSeconds(SecondsToReArrange);
        StartCoroutine(JumpToPlayer());
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player")) 
        {
            collision.GetComponent<IDamageable>().GetDamage(_meleeDamage);
        }
    }


    //Ranged
    void RandomRangeAttack() 
    {
        int rand = Random.Range(0, 2);
        if(rand == 0) 
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
        Instantiate(BigProjectile, LightTransform.position, Quaternion.identity).AddForce((target.position - transform.position).normalized * ForceSmallProjectile);
        yield return new WaitForSeconds(2);
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
}
