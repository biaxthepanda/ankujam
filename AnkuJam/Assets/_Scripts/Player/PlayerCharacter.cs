using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class PlayerCharacter : Character
{

    public Light2D PlayerLight;

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
}
