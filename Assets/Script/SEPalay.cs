using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SEPalay : MonoBehaviour
{
    [SerializeField] AudioSource source;
    public void Play(float volume)
    {
        source.Play();
    }
}
