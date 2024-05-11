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
}
