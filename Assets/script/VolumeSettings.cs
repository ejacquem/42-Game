using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VolumeSettings : MonoBehaviour
{
    [SerializeField] private AudioSource audioSrc;
    [SerializeField] private Slider volumeSlider;

    void Start()
    {
        SetMusicVolume(volumeSlider.value*0.5f);
        volumeSlider.onValueChanged.AddListener(SetMusicVolume);
    }

    void SetMusicVolume(float volume)
    {
        audioSrc.volume = volume*0.5f;
    }
}
