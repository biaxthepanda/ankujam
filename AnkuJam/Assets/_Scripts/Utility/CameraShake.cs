using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class CameraShake : MonoBehaviour
{


    CinemachineVirtualCamera cinemachine;

    private float timer;
    // Start is called before the first frame update
    void Awake()
    {
        cinemachine = GetComponent<CinemachineVirtualCamera>();
    }

    private void Start()
    {
      
    }

    // Update is called once per frame
    void Update()
    {
        if(timer > 0) 
        {
            timer -= Time.deltaTime;
            if(timer <= 0f) 
            {
                CinemachineBasicMultiChannelPerlin cinemachinePerlin = cinemachine.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
                cinemachinePerlin.m_AmplitudeGain = 0f;
            }
        }
    }

    public void ShakeCamera(float intensity, float time) 
    {
        CinemachineBasicMultiChannelPerlin cinemachinePerlin = cinemachine.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();

        cinemachinePerlin.m_AmplitudeGain = intensity;
        timer = time;
    }

   
}
