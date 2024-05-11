using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class AudioMixerHandler : MonoBehaviour
{
    public AudioMixer mixer;
    public Slider SFXSlider;
    public Slider MusicSlider;

    private void Awake()
    {
        MusicSlider.onValueChanged.AddListener(SetMusicVolume);
        SFXSlider.onValueChanged.AddListener(SetSFXVolume);
    }

    private void SetSFXVolume(float val)
    {
        mixer.SetFloat("SFXVolume", Mathf.Log10(val)*20);
    }

    private void SetMusicVolume(float val)
    {
        mixer.SetFloat("MusicVolume", Mathf.Log10(val) * 20);
    }
}
