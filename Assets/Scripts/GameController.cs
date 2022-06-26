using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using Random = System.Random;

public class GameController : MonoBehaviour
{
    public GameObject board;

    public int numberOfButtons = 6;
    public Slider buttonsSlider;
    
    public int sequenceLength = 4;
    public Slider sequenceSlider;

    public GameObject firstOpeningPanel;
    public GameObject midGamePanel;

    public static bool GameHasAlreadyStarted;

    private int _privateNumberOfButtons;
    public GameObject[] buttons;

    public Material[] materials;

    void Start()
    {
        _privateNumberOfButtons = board.transform.childCount;
        Debug.Log(_privateNumberOfButtons);
        buttons = new GameObject[_privateNumberOfButtons];

        for (int i = 0; i < _privateNumberOfButtons; i++)
        {
            buttons[i] = board.transform.GetChild(i).gameObject;
        }

        buttonsSlider.value = GetSliderValueFromButtonConfiguration(numberOfButtons);
        sequenceSlider.value = sequenceLength;

        GameHasAlreadyStarted = false;
    }

    void Update()
    {
        if (!UIController.GameIsPaused)
        {
            if (Input.GetMouseButtonDown(0))
            {
                ShuffleButtons();
            }
        }
    }

    public void GameLoop()
    {
        if (GameHasAlreadyStarted)
        {
            
        }
        else
        {
            GameHasAlreadyStarted = true;
            firstOpeningPanel.SetActive(false);
            UIController.GameIsPaused = false;
        }
    }

    public void ShuffleButtons()
    {
        Random random = new Random();
        var keys = materials.Select(e => random.NextDouble()).ToArray();

        Array.Sort(keys, materials);

        for (int i = 0; i < _privateNumberOfButtons; i++)
        {
            buttons[i].GetComponent<Renderer>().material = materials[i];
        }
    }

    public void UpdateNumberOfButtons(float newNumberOfButtons)
    {
        // Debug.Log("Updating number of buttons. New value is " + newNumberOfButtons);
        numberOfButtons = GetButtonConfigurationFromSlider((int) newNumberOfButtons);
    }

    public void UpdateSequenceLength(float newSequenceLength)
    {
        // Debug.Log("Updating sequence length. New value is " + newSequenceLength);
        sequenceLength = (int)newSequenceLength;
    }

    private int GetButtonConfigurationFromSlider(int sliderValue)
    {
        int valueToReturn = 0;
        if (sliderValue == 0)
        {
            valueToReturn = 4;
        }
        else if (sliderValue == 1)
        {
            valueToReturn = 6;
        }
        else if (sliderValue == 2)
        {
            valueToReturn = 9;
        }
        return valueToReturn;
    }

    private int GetSliderValueFromButtonConfiguration(int buttonConfiguration)
    {
        int valueToReturn = 0;
        if (buttonConfiguration == 4)
        {
            valueToReturn = 0;
        }
        else if (buttonConfiguration == 6)
        {
            valueToReturn = 1;
        }
        else if (buttonConfiguration == 9)
        {
            valueToReturn = 2;
        }
        return valueToReturn;
    }
}
