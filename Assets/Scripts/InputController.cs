using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.IO.Ports;

public class InputController : MonoBehaviour
{
    private bool _canCollectInput;

    private SerialPort stream = new SerialPort("COM9", 9600);
    
    public string inputStream;
    public string[] data;
    
    public bool arduinoInput;

    // EVENTS
    public delegate void ButtonPressedHandler(int buttonID, bool needIllumination);
    public static event ButtonPressedHandler ButtonPressed;

    private void Awake()
    {
        stream.WriteTimeout = 300;
        stream.ReadTimeout = 10;
        stream.DtrEnable = true;
        stream.RtsEnable = true;
        stream.Open();
        
        GameController.StartCollectingInput += OnStartCollectingInput;
        GameController.StopCollectingInput += OnStopCollectingInput;
        ButtonController.SendVibrateMotor += OnSendVibrateMotor;
    }

    private void OnDestroy()
    {
        GameController.StartCollectingInput -= OnStartCollectingInput;
        GameController.StopCollectingInput -= OnStopCollectingInput;
        ButtonController.SendVibrateMotor -= OnSendVibrateMotor;

        stream.Close();
    }
    
    void Start()
    {
        _canCollectInput = false;
    }
    
    void Update()
    {
        if (_canCollectInput)
        {
            if (arduinoInput)
            {
                try
                {
                    // reading the stream line
                    inputStream = stream.ReadLine();
                    
                    // splitting the input stream line with the character , in order to extract single button values
                    data = inputStream.Split(',');

                    // cycle through all the data entry (restricted to the number of active buttons) to see if a button was pressed
                    // we can skip consistency and duplicates controls: this logic is handled by the Arduino script
                    for (int i = 0; i < GameController.NumberOfButtons; i++)
                    {
                        if (Int32.Parse(data[i]) == 0)
                        {
                            ButtonPressed(i, true);
                        }
                    }
                }
                catch (TimeoutException) { }
            }
            else
            {
                if (Input.GetKeyDown(KeyCode.Q))
                {
                    Debug.Log("Pressed Q");
                    ButtonPressed(0, false);
                }
                if (Input.GetKeyDown(KeyCode.W))
                {
                    Debug.Log("Pressed W");
                    ButtonPressed(1, false);
                }
                if (Input.GetKeyDown(KeyCode.A))
                {
                    Debug.Log("Pressed A");
                    ButtonPressed(2, false);
                }
                if (Input.GetKeyDown(KeyCode.S))
                {
                    Debug.Log("Pressed S");
                    ButtonPressed(3, false);
                }
                if (Input.GetKeyDown(KeyCode.E))
                {
                    Debug.Log("Pressed E");
                    ButtonPressed(4, false);
                }
                if (Input.GetKeyDown(KeyCode.D))
                {
                    Debug.Log("Pressed D");
                    ButtonPressed(5, false);
                }
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

    private void OnSendVibrateMotor(int buttonID)
    {
        stream.WriteLine(buttonID.ToString());
    }
}
