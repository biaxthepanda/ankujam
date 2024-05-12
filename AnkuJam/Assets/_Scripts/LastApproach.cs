using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;
using UnityEngine.SceneManagement;

public class LastApproach : MonoBehaviour
{
    public GameObject Player;
    public Light2D Light;
    void Start()
    {
        Player = LevelManager.Player;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void StartAnim() 
    {
        Player.GetComponent<PlayerCharacter>().SR.flipX = false;
        Player.transform.DOMove(Light.transform.position, 5f).OnComplete(() => UIManager.Instance.ToggleLoadingScreen(true,1f)).OnComplete(()=>SceneManager.LoadScene(3)); ;
    }
}
