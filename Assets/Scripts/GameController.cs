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
    
    // BOOLEANS
    
    public static bool GameHasAlreadyStarted;
    private bool _playerIsPlaying;

    // EVENTS

    public delegate void SetupLevelHandler();
    public static event SetupLevelHandler SetupLevel;
    
    public delegate void StartTutorialHandler();
    public static event StartTutorialHandler StartTutorial;

    public delegate void ShowSequenceHandler();
    public static event ShowSequenceHandler ShowSequence;

    public delegate void ShowGameStartUIHandler();
    public static event ShowGameStartUIHandler ShowGameStartUI;

    public delegate void StartCollectingInputHandler();
    public static event StartCollectingInputHandler StartCollectingInput;

    public delegate void StopCollectingInputHandler();
    public static event StopCollectingInputHandler StopCollectingInput;

    // UI
    
    public GameObject firstOpeningPanel;
    public GameObject midGamePanel;

    private void Awake()
    {
        BoardController.SetupLevelEnded += OnSetupLevelEnded;
        BoardController.TutorialEnded += OnTutorialEnded;
        BoardController.ReadyToPlay += OnReadyToPlay;
        InputController.ButtonPressed += OnButtonPressed;
    }

    private void OnDestroy()
    {
        BoardController.SetupLevelEnded -= OnSetupLevelEnded;
        BoardController.TutorialEnded -= OnTutorialEnded;
        BoardController.ReadyToPlay -= OnReadyToPlay;
        InputController.ButtonPressed -= OnButtonPressed;
    }

    void Start()
    {
        buttonsSlider.value = GetSliderValueFromButtonConfiguration(NumberOfButtons);
        sequenceSlider.value = SequenceLength;
        GameHasAlreadyStarted = false;
        _playerIsPlaying = false;
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
        _playerIsPlaying = true;
        // set the pointer to the sequence to 0
        _pointerToSequence = 0;
        // show panel
        ShowGameStartUI?.Invoke();
        // invoke the input controller
        StartCollectingInput?.Invoke();
    }

    private void OnButtonPressed(int buttonID)
    {
        if (!UIController.GameIsPaused)
        {
            if (_playerIsPlaying)
            {
                Debug.Log("Checking if the button pressed corresponds to the right one in the sequence");

                Debug.Log("Current buttonID required: " + Sequence[_pointerToSequence]);
                Debug.Log("ButtonID pressed: " + buttonID);
                
                if (buttonID == Sequence[_pointerToSequence])
                {

                    Debug.Log("Got the right one");
                    
                    _pointerToSequence++;
                    
                    if (_pointerToSequence == SequenceLength)
                    {
                        Debug.Log("YOU WON!");
                        
                        _playerIsPlaying = false;

                        StopCollectingInput();

                        // show canvas
                    }
                }
                else
                {
                    Debug.Log("GAME OVER!");
                    
                    _playerIsPlaying = false;

                    StopCollectingInput();
                    
                    // show canvas
                }
            }
        }
    }

    public void GameLoop()
    {
        // TODO: delete this logic, only firstOpeningPanel is diplayed
        
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
