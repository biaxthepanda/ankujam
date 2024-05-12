using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class PlayerCharacter : Character
{
    public static Action OnPlayerDied;
    public Light2D PlayerLight;
    public bool IsDead = false;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public override void GetDamage(int damage)
    {
        Debug.Log("Player Got Damage");
        LevelManager.Instance.CamShake.ShakeCamera(3,0.1f);
        SoundManager.Instance.PlayOneShot(SoundManager.Sounds.damage);
        Health -= damage;
        if(Health<= 0 && !IsDead) 
        {
            IsDead = true;
            SoundManager.Instance.PlayOneShot(SoundManager.Sounds.deathMusic);
            ExpressionManager.Instance.CreateExpression("YOU'RE FIRED!",Color.red,2f);
            OnPlayerDied?.Invoke();
            RoomManager.Instance.FailedRoom();
        }
        UIManager.Instance.UpdateHealthBar(Health, MaxHealth);
        CharacterAnimator.SetTrigger("Hit");
        base.GetDamage(damage);
    }

    public void TogglePlayerLight(bool isOpen) 
    {
        if (isOpen) 
        {
            PlayerLight.intensity = 0.3f;
        }
        else
        {
            PlayerLight.intensity = 0;
        }
    
    }

    public void ResetPlayer() 
    {
        IsDead = false;
        Health = MaxHealth;
        UIManager.Instance.UpdateHealthBar(Health, MaxHealth);
    }

    public void AdjustHealth(float amount) 
    {
        Health += amount;
        UIManager.Instance.UpdateHealthBar(Health, MaxHealth);
    }
}   
