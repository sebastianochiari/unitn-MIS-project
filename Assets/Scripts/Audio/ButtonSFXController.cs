using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonSFXController : MonoBehaviour
{

    public AudioSource audioSouce;

    public AudioClip hoverSFX;
    public AudioClip clickSFX;

    public void OnButtonHover()
    {
        audioSouce.PlayOneShot(hoverSFX);
    }

    public void OnButtonClick()
    {
        audioSouce.PlayOneShot(clickSFX);
    }
}
