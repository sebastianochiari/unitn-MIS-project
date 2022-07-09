using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputController : MonoBehaviour
{

    private bool _canCollectInput;

    private void Awake()
    {
        GameController.StartCollectingInput += OnStartCollectingInput;
    }

    private void OnDestroy()
    {
        GameController.StartCollectingInput -= OnStartCollectingInput;
    }
    
    void Start()
    {
        _canCollectInput = false;
    }
    
    void Update()
    {
        if (_canCollectInput)
        {
            // inserire logica arduino
        }
    }

    private void OnStartCollectingInput()
    {
        _canCollectInput = true;
    }
}
