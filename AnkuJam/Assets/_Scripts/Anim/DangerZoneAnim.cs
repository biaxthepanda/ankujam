using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DangerZoneAnim : MonoBehaviour
{

    public SpriteRenderer SR;
    public float DangerSpeed;
    // Start is called before the first frame update
    void Start()
    {
        SR.material.color = Color.red;
        SR.material.DOFade(0.0f, DangerSpeed)
            .SetLoops(-1, LoopType.Yoyo);

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
