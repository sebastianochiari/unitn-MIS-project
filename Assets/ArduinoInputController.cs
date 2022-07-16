using System;
using System.Collections;
using System.Collections.Generic;
using System.IO.Ports;
using UnityEngine;

public class ArduinoInputController : MonoBehaviour
{
    
    private SerialPort stream = new SerialPort("COM9", 9600);

    public string inputStream;
    public string[] data;
    
    void Start()
    {
        stream.WriteTimeout = 300;
        stream.ReadTimeout = 5000;
        stream.DtrEnable = true;
        stream.RtsEnable = true;
        stream.Open();
        Debug.Log("SerialPort COM9 open: " + stream.IsOpen);
    }

    private void OnDestroy()
    {
        stream.Close();
    }
    
    void Update()
    {
        inputStream = stream.ReadLine();
        Debug.Log(inputStream);
    }
}
