using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyAfterSeconds : MonoBehaviour
{
    public float Seconds;
    void Start()
    {
        Invoke("DestroyObject",Seconds);
    }

    void DestroyObject() 
    {
        Destroy(gameObject);
    }
}
