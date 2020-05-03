using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AudioController : MonoBehaviour
{
    static float masterVolume = 1;
    static float musicVolume = 1;
    static float sfxVolume = 1;
    public List<AudioSource> music;
    public List<AudioSource> sfx;
    float musicPercent;
    float sfxPercent;
    public Slider masterSlider;
    public Slider musicSlider;
    public Slider sfxSlider;

    // Start is called before the first frame update
    void Start()
    {
        if (masterSlider != null)
        {
            SetSliders();
        }
            ApplySound();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetMasterVolume(System.Single value)
    {
        masterVolume = value;
        ApplySound();
    }

    public void SetMusicVolume(System.Single value)
    {
        musicVolume = value;
        ApplySound();
    }

    public void SetSFXVolume(System.Single value)
    {
        sfxVolume = value;
        ApplySound();
    }

    void ApplySound()
    {
        musicPercent = masterVolume * musicVolume;
        sfxPercent = masterVolume * sfxVolume;

        for(int i = 0; i < music.Count; i++)
        {
            music[i].volume = musicPercent;
        }
        for(int i = 0; i < sfx.Count; i++)
        {
            sfx[i].volume = sfxPercent;
        }
    }

    void SetSliders()
    {
        masterSlider.value = masterVolume;
        musicSlider.value = musicVolume;
        sfxSlider.value = sfxVolume;
    }
}
