using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoardController : MonoBehaviour
{
    public GameObject board;
    public static List<GameObject> Buttons;

    public Material material1;
    public Material material2;
    
    // EVENTS

    public delegate void SetupLevelEndedHandler();
    public static event SetupLevelEndedHandler SetupLevelEnded;

    public delegate void TutorialEndedHandler();
    public static event TutorialEndedHandler TutorialEnded;

    public delegate void ReadyToPlayHandler();
    public static event ReadyToPlayHandler ReadyToPlay;

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
        StartCoroutine(OnStartTutorialCoroutine());
    }

    IEnumerator OnStartTutorialCoroutine()
    {
        yield return new WaitForSeconds(2f);

        for (int i = 0; i < GameController.NumberOfButtons; i++)
        {
            Renderer buttonRenderer = Buttons[i].GetComponent<Renderer>();

            buttonRenderer.material = material1;
            
            yield return new WaitForSeconds(.25f);
            
            buttonRenderer.material = new Material(Shader.Find("Diffuse"));
            
            yield return new WaitForSeconds(2f);
        }

        TutorialEnded?.Invoke();
        
        yield break;
    }

    private void OnShowSequence()
    {
        Debug.Log("Start ShowSequenceButtons().");
        StartCoroutine(OnShowSequenceCoroutine());
    }

    IEnumerator OnShowSequenceCoroutine()
    {
        Debug.Log("Start ShowSequenceButtonsCoroutine().");
        
        yield return new WaitForSeconds(5f);
        
        for (int i = 0; i < GameController.SequenceLength; i++)
        {
            int buttonNumber = GameController.Sequence[i];

            Renderer buttonRenderer = Buttons[buttonNumber].GetComponent<Renderer>();

            buttonRenderer.material = material2;
            
            yield return new WaitForSeconds(1f);
            
            buttonRenderer.material = new Material(Shader.Find("Diffuse"));
            
            yield return new WaitForSeconds(2f);
        }

        ReadyToPlay?.Invoke();
        
        yield break;
    }
}
