using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using Random = System.Random;

public class GameController : MonoBehaviour
{
    public static int NumberOfButtons = 6;
    public Slider buttonsSlider;
    
    public static int SequenceLength = 4;
    public Slider sequenceSlider;

    public static int[] Sequence;

    private int _pointerToSequence;
    
    // Booleans
    
    public static bool GameHasAlreadyStarted;
    private bool playerIsPlaying;

    // Events

    public delegate void SetupLevelHandler();
    public static event SetupLevelHandler SetupLevel;
    
    public delegate void StartTutorialHandler();
    public static event StartTutorialHandler StartTutorial;

    public delegate void ShowSequenceHandler();
    public static event ShowSequenceHandler ShowSequence;

    public delegate void StartCollectingInputHandler();

    public static event StartCollectingInputHandler StartCollectingInput;

   

    // UI
    
    public GameObject firstOpeningPanel;
    public GameObject midGamePanel;

    private void Awake()
    {
        BoardController.SetupLevelEnded += OnSetupLevelEnded;
        BoardController.TutorialEnded += OnTutorialEnded;
        BoardController.ReadyToPlay += OnReadyToPlay;
    }

    private void OnDestroy()
    {
        BoardController.SetupLevelEnded -= OnSetupLevelEnded;
        BoardController.TutorialEnded += OnTutorialEnded;
        BoardController.ReadyToPlay -= OnReadyToPlay;
    }

    void Start()
    {
        buttonsSlider.value = GetSliderValueFromButtonConfiguration(NumberOfButtons);
        sequenceSlider.value = SequenceLength;
        GameHasAlreadyStarted = false;
        playerIsPlaying = false;
    }

    void Update()
    {
        if (!UIController.GameIsPaused)
        {
            if (playerIsPlaying)
            {
                
            }
        }
    }
    
    private void OnSetupLevelEnded()
    {
        StartTutorial?.Invoke();
    }
    
    private void OnTutorialEnded()
    {
        ShowSequence?.Invoke();
    }

    private void OnReadyToPlay()
    {
        // now the player is playing
        playerIsPlaying = true;
        // set the pointer to the sequence to 0
        _pointerToSequence = 0;
        // invoke the input controller
        StartCollectingInput?.Invoke();
    }

    public void GameLoop()
    {
        if (GameHasAlreadyStarted)
        {
            midGamePanel.SetActive(false);
        }
        else
        {
            GameHasAlreadyStarted = true;
            firstOpeningPanel.SetActive(false);
        }
        
        UIController.GameIsPaused = false;
            
        Debug.Log("Number of buttons: " + NumberOfButtons);
        Debug.Log("Sequence Length: " + SequenceLength);

        CreateSequence();

        SetupLevel?.Invoke();
    }

    private void CreateSequence()
    {
        Sequence = new int[SequenceLength];
        Random random = new Random();

        for (int i = 0; i < SequenceLength; i++)
        {
            Sequence[i] = random.Next(NumberOfButtons);
            Debug.Log("Sequence[" + i + "]: " + Sequence[i]);
        }
    }

    public void UpdateNumberOfButtons(float newNumberOfButtons)
    {
        // Debug.Log("Updating number of buttons. New value is " + newNumberOfButtons);
        NumberOfButtons = GetButtonConfigurationFromSlider((int) newNumberOfButtons);
    }

    public void UpdateSequenceLength(float newSequenceLength)
    {
        // Debug.Log("Updating sequence length. New value is " + newSequenceLength);
        SequenceLength = (int)newSequenceLength;
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
