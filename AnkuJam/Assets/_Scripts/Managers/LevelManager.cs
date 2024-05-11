using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class LevelManager : MonoBehaviour
{
    #region Singleton
    public static LevelManager Instance { get; private set; }

    private void Awake()
    {
        // Bir örnek varsa ve ben deðilse, yoket.

        if (Instance != null && Instance != this)
        {
            return;
        }
        Instance = this;
    }

    #endregion

    public static GameObject Player;
    public Light2D GlobalLight;
    public CameraShake CamShake;

    // Start is called before the first frame update
    void Start()
    {
        Player = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ToggleGlobalLight(bool isOpen,float time) 
    {
        if (!isOpen) 
        {
            DOTween.To(x => GlobalLight.intensity = x, 1, 0, time);
        }
        else 
        {
            DOTween.To(x => GlobalLight.intensity = x, 0, 1, time);
        }
    }
}
