using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExtraHealth : MonoBehaviour
{

    public float HealthAmount;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player")) 
        {
            collision.GetComponent<PlayerCharacter>().AdjustHealth(HealthAmount);
            SoundManager.Instance.PlayOneShot(SoundManager.Sounds.healthSFX);
            Destroy(gameObject);
        }
    }
}
