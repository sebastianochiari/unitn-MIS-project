using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UISliderSFX : MonoBehaviour
{
    public AudioSource audioSource;

    public AudioClip audioClip;

    public void OnSliderValueChanged()
    {
        audioSource.PlayOneShot(audioClip);
    }
}
