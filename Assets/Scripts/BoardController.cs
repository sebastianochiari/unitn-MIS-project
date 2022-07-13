using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class BoardController : MonoBehaviour
{
    public GameObject board;
    public static List<GameObject> Buttons;

    public Material material1;
    public Material material2;
    
    // EVENTS

    public delegate void PlayTutorialButtonHandler(int buttonID, bool isTutorial);
    public static event PlayTutorialButtonHandler PlayTutorialButton;

    public delegate void SetupLevelEndedHandler();
    public static event SetupLevelEndedHandler SetupLevelEnded;

    public delegate void TutorialEndedHandler();
    public static event TutorialEndedHandler TutorialEnded;

    public delegate void ReadyToPlayHandler();
    public static event ReadyToPlayHandler ReadyToPlay;

    public delegate void ShowTutorialUIHandler();
    public static event ShowTutorialUIHandler ShowTutorialUI;

    public delegate void ShowSequenceUIHandler();
    public static event ShowSequenceUIHandler ShowSequenceUI;

    private void Awake()
    {
        GameController.SetupLevel += OnSetupLevel;
        GameController.StartTutorial += OnStartTutorial;
        GameController.ShowSequence += OnShowSequence;
    }
    
    private void OnDestroy()
    {
        GameController.SetupLevel -= OnSetupLevel;
        GameController.StartTutorial -= OnStartTutorial;
        GameController.ShowSequence += OnShowSequence;
    }

    private void Start()
    {
        Buttons = new List<GameObject>();

        foreach (Transform child in board.transform)
        {
            if (child.CompareTag("Button"))
            {
                Buttons.Add(child.gameObject);
            }
        }
    }

    private void OnSetupLevel()
    {
        for (int i = 0; i < GameController.NumberOfButtons; i++)
        {
            Buttons[i].SetActive(true);
        }

        SetupLevelEnded?.Invoke();
    }
    
    private void OnStartTutorial()
    {
        ShowTutorialUI?.Invoke();
        StartCoroutine(OnStartTutorialCoroutine());
    }

    IEnumerator OnStartTutorialCoroutine()
    {
        yield return new WaitForSeconds(12f);

        for (int i = 0; i < GameController.NumberOfButtons; i++)
        {
            // send event to buttons with buttonID
            PlayTutorialButton(i, true);
            
            yield return new WaitForSeconds(5f);
        }

        TutorialEnded?.Invoke();
        
        yield break;
    }

    private void OnShowSequence()
    {
        ShowSequenceUI?.Invoke();
        StartCoroutine(OnShowSequenceCoroutine());
    }

    IEnumerator OnShowSequenceCoroutine()
    {

        yield return new WaitForSeconds(12f);
        
        for (int i = 0; i < GameController.SequenceLength; i++)
        {
            // send event to buttons with buttonID
            PlayTutorialButton(GameController.Sequence[i], false);
            
            yield return new WaitForSeconds(5f);
        }

        ReadyToPlay?.Invoke();
        
        yield break;
    }
}
