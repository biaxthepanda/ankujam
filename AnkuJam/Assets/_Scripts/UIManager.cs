using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{

    #region Singleton
    public static UIManager Instance { get; private set; }

    private void Awake()
    {
        // Bir �rnek varsa ve ben de�ilse, yoket.

        if (Instance != null && Instance != this)
        {
            return;
        }
        Instance = this;
    }

    #endregion

    public Image HealthBarImage;
    public Image DashImage;
    public Image BossHealthBar;
    public GameObject BossHealthBarBack;
    public Image LoadingScreen;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void UpdateHealthBar(float health,float maxhealth) 
    {
        HealthBarImage.fillAmount = health / maxhealth;
    }

    public void ToggleDashImage(bool isOpen) 
    {
        DashImage.gameObject.SetActive(isOpen);
    }

    public void ToggleBossHealth(bool isOpen) 
    {
        BossHealthBarBack.gameObject.SetActive(isOpen);
    }

    public void UpdateBossHealthBar(float amount) 
    {
        BossHealthBar.fillAmount = amount;
    }

    public void ToggleLoadingScreen(bool isOpen,float duration = 2f) 
    {
        if (isOpen) 
        {
            LoadingScreen.DOFade(1f, duration);
        
        }
        else
        {
            LoadingScreen.DOFade(0, duration);
        
        }
    }
}
