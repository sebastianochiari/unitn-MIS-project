using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonController : MonoBehaviour
{

    public int buttonID;

    public bool visualButton;
    public bool audioButton;
    public bool shakyButton;

    public bool stereoButton;
    public bool rightButton;
    public bool leftButton;

    public AudioClip audioClip;
    public AudioSource audioSource;

    private GameObject _borderGameObject;
    
    private void Awake()
    {
        // audio button initialization
        if (audioButton)
        {
            audioSource.clip = audioClip;
            if (rightButton)
            {
                audioSource.panStereo = 1.0f;
            } 
            else if (leftButton)
            {
                audioSource.panStereo = -1.0f;
            }
        }
        
        // events initialization
        BoardController.PlayTutorialButton += OnPlayButton;
        InputController.ButtonPressed += OnPlayButton;
    }

    private void OnEnable()
    {
        _borderGameObject = this.transform.Find("Border").gameObject;
        _borderGameObject.SetActive(false);
    }

    private void OnDisable()
    {
        BoardController.PlayTutorialButton -= OnPlayButton;
    }

    private void OnPlayButton(int id, bool isTutorial)
    {
        if (id == buttonID)
        {
            if (isTutorial || visualButton)
            {
                StartCoroutine(FlashBorderCoroutine());
            }
            if (audioButton)
            {
                audioSource.PlayOneShot(audioClip);
            }
            if (shakyButton)
            {
                // send arduino event
            }
        }
    }

    IEnumerator FlashBorderCoroutine()
    {
        _borderGameObject.SetActive(true);
        
        yield return new WaitForSeconds(3f);

        _borderGameObject.SetActive(false);
    }
}
