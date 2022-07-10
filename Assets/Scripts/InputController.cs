using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputController : MonoBehaviour
{

    private bool _canCollectInput;
    
    // EVENTS
    public delegate void ButtonPressedHandler(int buttonID);
    public static event ButtonPressedHandler ButtonPressed;

    private void Awake()
    {
        GameController.StartCollectingInput += OnStartCollectingInput;
        GameController.StopCollectingInput += OnStopCollectingInput;
    }

    private void OnDestroy()
    {
        GameController.StartCollectingInput -= OnStartCollectingInput;
        GameController.StopCollectingInput -= OnStopCollectingInput;
    }
    
    void Start()
    {
        _canCollectInput = false;
    }
    
    void Update()
    {
        if (_canCollectInput)
        {
            
            // TODO INSERIRE LOGICA ARDUINO
            
            if (Input.GetKeyDown(KeyCode.Q))
            {
                Debug.Log("Pressed Q");
                ButtonPressed(0);
            }
            if (Input.GetKeyDown(KeyCode.W))
            {
                Debug.Log("Pressed W");
                ButtonPressed(1);
            }
            if (Input.GetKeyDown(KeyCode.A))
            {
                Debug.Log("Pressed A");
                ButtonPressed(2);
            }
            if (Input.GetKeyDown(KeyCode.S))
            {
                Debug.Log("Pressed S");
                ButtonPressed(3);
            }
        }
    }

    private void OnStartCollectingInput()
    {
        _canCollectInput = true;
    }

    private void OnStopCollectingInput()
    {
        _canCollectInput = false;
    }
}
