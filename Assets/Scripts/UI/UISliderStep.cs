using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UISliderStep : MonoBehaviour
{
    Slider slider = null;
    int stepAmount = 10;
    int numberOfSteps = 0;

    public AudioSource audioSource;
    public AudioClip audioClip;

    void Start()
    {
        slider = GetComponent<Slider>();
        numberOfSteps = (int) slider.maxValue / stepAmount;
    }
    
    public void UpdateStep()
    {
        float range = (slider.value / slider.maxValue) * numberOfSteps;
        int ceil = Mathf.CeilToInt(range);
        slider.value = ceil * stepAmount;
        
        audioSource.PlayOneShot(audioClip);
    }
}

