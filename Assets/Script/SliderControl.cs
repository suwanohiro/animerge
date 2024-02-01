using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class SliderControl : MonoBehaviour
{

    [SerializeField] AudioMixer audioMixer;
    [SerializeField] AudioMixerGroup sound;
    [SerializeField] Slider BGMslider;

    void Start()
    {
        //sound
        audioMixer.GetFloat(sound.name, out float Volume);
        BGMslider.value = Volume;
    }
    public void ChengeVolume(float volume)
    {
        audioMixer.SetFloat(sound.name, volume);
    }
}
