using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleManager : MonoBehaviour
{

    #region Singleton
    public static ParticleManager Instance { get; private set; }

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

    public ParticleSystem BubbleParticle;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SpawnParticleAtLocation(ParticleSystem particleSystem,Vector2 Loc) 
    {
        Instantiate(particleSystem, Loc, Quaternion.identity);
        
    }
}
