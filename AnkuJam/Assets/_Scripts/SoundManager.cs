using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    #region Singleton
    public static SoundManager Instance { get; private set; }

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

    public AudioClip[] AudioClips;

    public enum Sounds 
    {
        tossing = 0,
        throwingBomb = 1,
        scenePassC5 = 2,
        scenePassa4 = 3,
        playerDeath = 4,
        menuOpen = 5,
        menuClose = 6,
        menuButtonSound3 = 7,
        menuButtonSound2 = 8,
        menuButtonSound1 = 9,
        mainTheme = 10,
        Gunshot = 11,
        gillWater = 12,
        gill = 13,
        explosionJellyfish = 14,
        explosion = 15,
        deathMusic = 16,
        BossFight_2Opening = 17,
        Bossfight_2mid = 18,
        Bossfight_2end = 19,

    }

    

}
